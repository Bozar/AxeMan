using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface INPCAttack
    {
        int AttackRange { get; }
    }

    public class NPCAttack : MonoBehaviour, INPCAttack
    {
        private int baseAttackRange;

        public int AttackRange
        {
            get
            {
                // TODO: Change data based on actor status.
                return baseAttackRange;
            }
        }

        private void Awake()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            ActorDataTag dataTag = ActorDataTag.AttackRange;

            baseAttackRange = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(mainTag, subTag, dataTag);
        }
    }
}
