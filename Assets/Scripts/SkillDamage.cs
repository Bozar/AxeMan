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
        private Dictionary<SkillNameTag, int> damageDict;

        public int GetSkillDamage(SkillNameTag skillNameTag)
        {
            int invalidDamage = -99;

            if (damageDict.TryGetValue(skillNameTag, out int damage))
            {
                return damage;
            }
            return invalidDamage;
        }

        public int GetSkillDamage(CommandTag commandTag)
        {
            return GetSkillDamage(
                GameCore.AxeManCore.GetComponent<ConvertSkillMetaInfo>()
                .GetSkillNameTag(commandTag));
        }

        private void Awake()
        {
            damageDict = new Dictionary<SkillNameTag, int>
            {
                { SkillNameTag.Q, 3 }, { SkillNameTag.W, 2 },
                { SkillNameTag.E, 0 }, { SkillNameTag.R, 1 }
            };
        }

        private void Start()
        {
        }
    }
}
