using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface INPCAttack
    {
        int AttackRange { get; }

        int Damage { get; }
    }

    public class NPCAttack : MonoBehaviour, INPCAttack
    {
        private int baseAttackRange;
        private int baseDamage;

        public int AttackRange
        {
            get
            {
                // TODO: Change data based on actor status.
                return baseAttackRange;
            }
        }

        public int Damage
        {
            get
            {
                // TODO: Change data based on actor status.
                return baseDamage;
            }
        }

        private void Awake()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            ActorDataTag range = ActorDataTag.AttackRange;
            ActorDataTag damage = ActorDataTag.Damage;

            baseAttackRange = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(mainTag, subTag, range);
            baseDamage = GameCore.AxeManCore.GetComponent<ActorData>()
               .GetIntData(mainTag, subTag, damage);
        }
    }
}
