using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class VerifySkillRange : MonoBehaviour
    {
        private GameObject aimMarker;
        private GameObject pc;

        private void Start()
        {
            GetComponent<Wizard>().CreatedWorld
                += VerifySkillRange_CreatedWorld;
            GetComponent<AimMode>().VerifyingSkill
                += VerifySkillRange_VerifyingSkill;
        }

        private void VerifySkillRange_CreatedWorld(object sender, EventArgs e)
        {
            aimMarker = GetComponent<SearchObject>().Search(SubTag.AimMarker)[0];
            pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
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
