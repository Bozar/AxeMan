using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
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

    public class AltarCooldown : MonoBehaviour, IAltarCooldown
    {
        public int CurrentCooldown { get; private set; }

        public int MaxCooldown { get; private set; }

        public int MinCooldown { get; private set; }

        private void AltarCooldown_CreatedWorld(object sender, EventArgs e)
        {
            MaxCooldown = GetComponent<ActorData>().GetIntData(
                MainTag.Building, SubTag.DEFAULT, ActorDataTag.Cooldown);
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
            }
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
            GetComponent<InitializeMainGame>().CreatedWorld
                += AltarCooldown_CreatedWorld;
        }
    }
}
