using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCTeleportSelf : MonoBehaviour
    {
        private GameObject aimMarker;

        private void PCTeleportSelf_CreatedWorld(object sender, EventArgs e)
        {
            aimMarker = GameCore.AxeManCore.GetComponent<SearchObject>()
                .Search(SubTag.AimMarker)[0];
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
            GameCore.AxeManCore.GetComponent<Wizard>().CreatedWorld
                += PCTeleportSelf_CreatedWorld;
            GameCore.AxeManCore.GetComponent<PublishAction>().TakingAction
                 += PCTeleportSelf_TakingAction;
        }
    }
}
