using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class VerifySkillRange : MonoBehaviour
    {
        private GameObject aimMarker;
        private GameObject pc;

        private void Start()
        {
            GetComponent<InitializeMainGame>().SettingReference
                += VerifySkillRange_SettingReference;
            GetComponent<AimMode>().VerifyingSkill
                += VerifySkillRange_VerifyingSkill;
        }

        private void VerifySkillRange_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            aimMarker = e.AimMarker;
            pc = e.PC;
        }

        private void VerifySkillRange_VerifyingSkill(object sender,
            VerifyingSkillEventArgs e)
        {
            int[] markerPos = aimMarker.GetComponent<MetaInfo>().Position;
            int[] pcPos = pc.GetComponent<MetaInfo>().Position;
            int distance = GetComponent<Distance>().GetDistance(pcPos, markerPos);

            PCSkillManager skillManager = pc.GetComponent<PCSkillManager>();
            SkillNameTag skillName = skillManager.GetSkillNameTag(e.UseSkill);
            int skillRange = skillManager.GetSkillRange(skillName);

            bool canUseSkill = distance <= skillRange;
            if (!canUseSkill)
            {
                e.CanUseSkill.Push(false);
            }
        }
    }
}
