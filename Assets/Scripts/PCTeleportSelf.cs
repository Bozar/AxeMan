using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.InitializeGameWorld;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCTeleportSelf : MonoBehaviour
    {
        private GameObject aimMarker;

        private void PCTeleportSelf_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            aimMarker = e.AimMarker;
        }

        private void PCTeleportSelf_TakingAction(object sender,
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
            if (skillType != SkillTypeTag.Move)
            {
                return;
            }

            int[] source = GetComponent<MetaInfo>().Position;
            int[] target = aimMarker.GetComponent<MetaInfo>().Position;

            GetComponent<LocalManager>().SetPosition(target);
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(source);
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(target);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<InitializeMainGame>()
                .SettingReference += PCTeleportSelf_SettingReference;
            GameCore.AxeManCore.GetComponent<PublishAction>().TakingAction
                 += PCTeleportSelf_TakingAction;
        }
    }
}
