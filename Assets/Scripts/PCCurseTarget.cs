using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCCurseTarget : MonoBehaviour
    {
        private GameObject aimMarker;
        private Dictionary<SkillComponentTag, SkillComponentTag> curse2flaw;

        private void Awake()
        {
            curse2flaw = new Dictionary<SkillComponentTag, SkillComponentTag>()
            {
                { SkillComponentTag.FireCurse, SkillComponentTag.FireFlaw },
                { SkillComponentTag.WaterCurse, SkillComponentTag.WaterFlaw },
                { SkillComponentTag.AirCurse, SkillComponentTag.AirFlaw },
                { SkillComponentTag.EarthCurse, SkillComponentTag.EarthFlaw },
            };
        }

        private void PCCurseTarget_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            aimMarker = e.AimMarker;
        }

        private void PCCurseTarget_TakingAction(object sender,
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
            if (skillType != SkillTypeTag.Curse)
            {
                return;
            }

            Dictionary<SkillComponentTag, EffectData> compInt
                = GetComponent<PCSkillManager>().GetSkillEffect(skillName);
            int[] position = aimMarker.GetComponent<MetaInfo>().Position;

            if (!GameCore.AxeManCore.GetComponent<SearchObject>()
                .Search(position[0], position[1], MainTag.Actor,
                out GameObject[] targets))
            {
                return;
            }

            SkillComponentTag flaw;
            foreach (SkillComponentTag curse in curse2flaw.Keys)
            {
                if (compInt.ContainsKey(curse))
                {
                    flaw = curse2flaw[curse];
                    targets[0].GetComponent<ActorStatus>().AddStatus(
                        flaw, compInt[curse]);
                }
            }
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<InitializeMainGame>()
                .SettingReference += PCCurseTarget_SettingReference;
            GameCore.AxeManCore.GetComponent<PublishAction>().TakingAction
                += PCCurseTarget_TakingAction;
        }
    }
}
