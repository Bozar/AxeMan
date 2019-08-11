using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCAttackTarget : MonoBehaviour
    {
        private GameObject aimMarker;

        private void PCAttackTarget_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            aimMarker = e.AimMarker;
        }

        private void PCAttackTarget_TakingAction(object sender,
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
            if (skillType != SkillTypeTag.Attack)
            {
                return;
            }

            int[] position = aimMarker.GetComponent<MetaInfo>().Position;
            if (!GameCore.AxeManCore.GetComponent<SearchObject>()
                .Search(position[0], position[1], MainTag.Actor,
                out GameObject[] targets))
            {
                return;
            }

            int damage = GetComponent<PCSkillManager>().GetSkillDamage(skillName);
            targets[0].GetComponent<HP>().Subtract(damage);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<Wizard>().SettingReference
                += PCAttackTarget_SettingReference;
            GameCore.AxeManCore.GetComponent<PublishAction>().TakingAction
                += PCAttackTarget_TakingAction;
        }
    }
}
