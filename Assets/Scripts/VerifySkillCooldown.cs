using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class VerifySkillCooldown : MonoBehaviour
    {
        private PCSkillManager skillManager;

        private void Start()
        {
            GetComponent<Wizard>().CreatedWorld
                += VerifySkillCooldown_CreatedWorld;
            GetComponent<AimMode>().VerifyingSkill
                += VerifySkillCooldown_VerifyingSkill;
        }

        private void VerifySkillCooldown_CreatedWorld(object sender, EventArgs e)
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            skillManager = pc.GetComponent<PCSkillManager>();
        }

        private void VerifySkillCooldown_VerifyingSkill(object sender,
            VerifyingSkillEventArgs e)
        {
            bool canUseSkill
                = skillManager.GetCurrentCooldown(e.UseSkill)
                <= skillManager.MinCooldown;

            if (!canUseSkill)
            {
                e.CanUseSkill.Push(false);
            }
        }
    }
}
