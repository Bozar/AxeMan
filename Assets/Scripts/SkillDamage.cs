using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillDamage
    {
        int GetSkillDamage(SkillNameTag skillNameTag);

        int GetSkillDamage(CommandTag commandTag);
    }

    public class SkillDamage : MonoBehaviour, ISkillDamage
    {
        private int baseDamage;
        private int invalidDamage;
        private Dictionary<SkillNameTag, int> nameDamage;
        private int zeroDamage;

        public int GetSkillDamage(SkillNameTag skillNameTag)
        {
            if (!nameDamage.ContainsKey(skillNameTag))
            {
                return invalidDamage;
            }

            int damage = nameDamage[skillNameTag];
            if (damage == invalidDamage)
            {
                damage = SetBaseDamage(skillNameTag);
            }

            // TODO: Change damage based on PC status.

            return damage;
        }

        public int GetSkillDamage(CommandTag commandTag)
        {
            return GetSkillDamage(
                GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetSkillNameTag(commandTag));
        }

        private void Awake()
        {
            invalidDamage = -99;
            zeroDamage = 0;
            baseDamage = 1;

            nameDamage = new Dictionary<SkillNameTag, int>
            {
                { SkillNameTag.Q, invalidDamage },
                { SkillNameTag.W, invalidDamage },
                { SkillNameTag.E, invalidDamage },
                { SkillNameTag.R, invalidDamage },
            };
        }

        private int SetBaseDamage(SkillNameTag skillNameTag)
        {
            SkillTypeTag skillType = GetComponent<PCSkillManager>()
                .GetSkillTypeTag(skillNameTag);
            int damage = zeroDamage;

            Dictionary<SkillComponentTag, int[]> compEffect;
            SkillComponentTag checkComp;

            if (skillType == SkillTypeTag.Attack)
            {
                damage = baseDamage;
                compEffect = GetComponent<PCSkillManager>()
                    .GetSkillEffect(skillNameTag);
                checkComp = SkillComponentTag.AirCurse;

                if (compEffect.TryGetValue(checkComp, out int[] powerDuration))
                {
                    damage += powerDuration[0];
                }
            }

            nameDamage[skillNameTag] = damage;
            return damage;
        }

        private void Start()
        {
        }
    }
}
