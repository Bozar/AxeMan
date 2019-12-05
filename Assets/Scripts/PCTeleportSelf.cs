using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCTeleportSelf : MonoBehaviour
    {
        private GameObject aimMarker;
        private int[] targetPosition;

        private void PCTeleportSelf_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            aimMarker = e.AimMarker;
        }

        private void PCTeleportSelf_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.LeaveMode == GameModeTag.AimMode)
            {
                targetPosition = aimMarker.GetComponent<MetaInfo>().Position;
            }
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
            int[] target = targetPosition;

            GetComponent<LocalManager>().SetPosition(target);
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(source);
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(target);

            GameCore.AxeManCore.GetComponent<LogManager>().Add(
                new LogMessage(LogCategoryTag.Combat, LogMessageTag.PCTeleport));
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<InitializeMainGame>()
                .SettingReference += PCTeleportSelf_SettingReference;
            GameCore.AxeManCore.GetComponent<PublishAction>().TakingAction
                 += PCTeleportSelf_TakingAction;
            GameCore.AxeManCore.GetComponent<GameModeManager>().SwitchingGameMode
                += PCTeleportSelf_SwitchingGameMode;
        }
    }
}
