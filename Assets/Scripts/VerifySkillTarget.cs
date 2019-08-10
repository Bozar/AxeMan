using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class VerifySkillTarget : MonoBehaviour
    {
        private GameObject aimMarker;
        private PCSkillManager skillManager;

        private void Start()
        {
            GetComponent<Wizard>().CreatedWorld
                += VerifySkillTarget_CreatedWorld;
            GetComponent<AimMode>().VerifyingSkill
                += VerifySkillTarget_VerifyingSkill;
        }

        private bool VerifyAttackCurse(GameObject[] gameObjects)
        {
            foreach (GameObject go in gameObjects)
            {
                if ((go.GetComponent<MetaInfo>().MainTag == MainTag.Actor)
                    && (go.GetComponent<MetaInfo>().SubTag != SubTag.PC))
                {
                    return true;
                }
            }
            return false;
        }

        private bool VerifyBuff(GameObject[] gameObjects)
        {
            foreach (GameObject go in gameObjects)
            {
                if (go.GetComponent<MetaInfo>().SubTag == SubTag.PC)
                {
                    return true;
                }
            }
            return false;
        }

        private bool VerifyMove(GameObject[] gameObjects)
        {
            foreach (GameObject go in gameObjects)
            {
                if ((go.GetComponent<MetaInfo>().MainTag == MainTag.Actor)
                    || (go.GetComponent<MetaInfo>().MainTag == MainTag.Building))
                {
                    return false;
                }
            }
            return true;
        }

        private void VerifySkillTarget_CreatedWorld(object sender, EventArgs e)
        {
            aimMarker = GetComponent<SearchObject>().Search(SubTag.AimMarker)[0];
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            skillManager = pc.GetComponent<PCSkillManager>();
        }

        private void VerifySkillTarget_VerifyingSkill(object sender,
            VerifyingSkillEventArgs e)
        {
            SkillNameTag skillName = skillManager.GetSkillNameTag(e.UseSkill);
            SkillTypeTag skillType = skillManager.GetSkillTypeTag(skillName);

            int[] position = aimMarker.GetComponent<MetaInfo>().Position;
            GameObject[] go = GetComponent<SearchObject>().Search(
                position[0], position[1]);

            bool canUseSkill;

            switch (skillType)
            {
                case SkillTypeTag.Move:
                    canUseSkill = VerifyMove(go);
                    break;

                case SkillTypeTag.Attack:
                case SkillTypeTag.Curse:
                    canUseSkill = VerifyAttackCurse(go);
                    break;

                case SkillTypeTag.Buff:
                    canUseSkill = VerifyBuff(go);
                    break;

                default:
                    canUseSkill = false;
                    break;
            }

            if (!canUseSkill)
            {
                e.CanUseSkill.Push(false);
            }
        }
    }
}
