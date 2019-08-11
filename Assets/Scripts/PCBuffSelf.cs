using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCBuffSelf : MonoBehaviour
    {
        private SkillComponentTag[] buffComps;

        private void Awake()
        {
            buffComps = new SkillComponentTag[]
            {
                SkillComponentTag.FireMerit,
                SkillComponentTag.WaterMerit,
                SkillComponentTag.AirMerit,
                SkillComponentTag.EarthMerit,
            };
        }

        private void PCBuffSelf_TakingAction(object sender,
            PublishActionEventArgs e)
        {
            if (e.SubTag != SubTag.PC)
            {
                return;
            }

            SkillNameTag skillName = GetComponent<PCSkillManager>()
               .GetSkillNameTag(e.Action);
            SkillTypeTag skillType = GetComponent<PCSkillManager>()
               .GetSkillTypeTag(skillName);
            if (skillType != SkillTypeTag.Buff)
            {
                return;
            }

            Dictionary<SkillComponentTag, EffectData> compInt
                = GetComponent<PCSkillManager>().GetSkillEffect(skillName);

            foreach (SkillComponentTag sct in buffComps)
            {
                if (compInt.ContainsKey(sct))
                {
                    GetComponent<ActorStatus>().AddStatus(sct, compInt[sct]);
                }
            }
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PublishAction>().TakingAction
                += PCBuffSelf_TakingAction;
        }
    }
}
