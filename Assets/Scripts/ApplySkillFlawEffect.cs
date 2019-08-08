using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public class ApplySkillFlawEffect : MonoBehaviour
    {
        private SkillComponentTag[] skillComps;

        private void ApplySkillFlawEffect_TakenAction(object sender,
            PublishActionEventArgs e)
        {
            if (e.SubTag != SubTag.PC)
            {
                return;
            }

            SkillNameTag skillName
                = GetComponent<PCSkillManager>().GetSkillNameTag(e.Action);
            if (skillName == SkillNameTag.INVALID)
            {
                return;
            }

            Dictionary<SkillComponentTag, EffectData> compInt
                = GetComponent<PCSkillManager>().GetSkillEffect(skillName);
            foreach (SkillComponentTag sct in skillComps)
            {
                if (compInt.ContainsKey(sct))
                {
                    GetComponent<ActorStatus>().AddStatus(sct, compInt[sct]);
                }
            }
        }

        private void Awake()
        {
            skillComps = new SkillComponentTag[]
            {
                SkillComponentTag.FireFlaw, SkillComponentTag.WaterFlaw,
                SkillComponentTag.AirFlaw, SkillComponentTag.EarthFlaw,
            };
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PublishAction>().TakenAction
                += ApplySkillFlawEffect_TakenAction;
        }
    }
}
