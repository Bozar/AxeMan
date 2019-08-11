using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameMode;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class VerifySkillCooldown : MonoBehaviour
    {
        private PCSkillManager skillManager;

        private void Start()
        {
            GetComponent<Wizard>().SettingReference
                += VerifySkillCooldown_SettingReference;
            GetComponent<AimMode>().VerifyingSkill
                += VerifySkillCooldown_VerifyingSkill;
        }

        private void VerifySkillCooldown_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            skillManager = e.PC.GetComponent<PCSkillManager>();
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
