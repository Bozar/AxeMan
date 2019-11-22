using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SchedulingSystem;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IAltarCooldown
    {
        int CurrentCooldown { get; }

        int MaxCooldown { get; }

        int MinCooldown { get; }
    }

    // All altars share the same cooldown counter. So this component is attached
    // to GameCore.AxeManCore.
    public class AltarCooldown : MonoBehaviour, IAltarCooldown
    {
        private int addCooldown;

        public event EventHandler<EventArgs> ChangedAltarCooldown;

        public int CurrentCooldown { get; private set; }

        public int MaxCooldown { get; private set; }

        public int MinCooldown { get; private set; }

        protected virtual void OnChangedAltarCooldown(EventArgs e)
        {
            ChangedAltarCooldown?.Invoke(this, e);
        }

        private void AltarCooldown_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            MaxCooldown = GetComponent<ActorData>().GetIntData(
                MainTag.Altar, SubTag.DEFAULT, ActorDataTag.Cooldown);
            addCooldown = GetComponent<ActorData>().GetIntData(
                MainTag.Altar, SubTag.DEFAULT, ActorDataTag.AddCooldown);
        }

        private void AltarCooldown_StartingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (e.SubTag != SubTag.PC)
            {
                return;
            }

            if (CurrentCooldown > 0)
            {
                CurrentCooldown--;
                CurrentCooldown = Math.Max(MinCooldown, CurrentCooldown);
                OnChangedAltarCooldown(EventArgs.Empty);
            }
        }

        private void AltarCooldown_TakenAction(object sender,
            PublishActionEventArgs e)
        {
            if ((e.SubTag != SubTag.PC) || (e.Action != ActionTag.ActiveAltar))
            {
                return;
            }
            CurrentCooldown = MaxCooldown;
            OnChangedAltarCooldown(EventArgs.Empty);
        }

        private void AltarCooldown_UpgradingAltar(object sender, EventArgs e)
        {
            MaxCooldown += addCooldown;
            CurrentCooldown = MaxCooldown;
            OnChangedAltarCooldown(EventArgs.Empty);
        }

        private void Awake()
        {
            MinCooldown = 0;
            CurrentCooldown = MinCooldown;
        }

        private void Start()
        {
            GetComponent<TurnManager>().StartingTurn
                += AltarCooldown_StartingTurn;
            GetComponent<InitializeMainGame>().SettingReference
                += AltarCooldown_SettingReference;
            GetComponent<PublishAction>().TakenAction
                += AltarCooldown_TakenAction;
            GetComponent<UpgradeAltar>().UpgradingAltar
                += AltarCooldown_UpgradingAltar;
        }
    }
}
