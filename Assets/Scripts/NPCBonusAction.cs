using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface INPCBonusAction
    {
        int CurrentCooldown { get; }

        int MaxCooldown { get; }

        int MinCooldown { get; }
    }

    public class NPCBonusAction : MonoBehaviour, INPCBonusAction
    {
        // TODO: Count down every turn.
        public int CurrentCooldown { get; private set; }

        public int MaxCooldown { get; private set; }

        public int MinCooldown { get; private set; }

        private void Awake()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            ActorDataTag dataTag = ActorDataTag.Cooldown;

            MaxCooldown = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(mainTag, subTag, dataTag);
            CurrentCooldown = MaxCooldown;
            MinCooldown = 0;
        }
    }
}
