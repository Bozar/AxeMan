using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCActiveAltar : MonoBehaviour
    {
        private void PCActiveAltar_BlockingPCMovement(object sender,
            BlockPCMovementEventArgs e)
        {
            // Check if PC bumps into an altar.
            int x = e.TargetPosition[0];
            int y = e.TargetPosition[1];
            if (!GameCore.AxeManCore.GetComponent<SearchObject>().Search(
                x, y, MainTag.Building, out GameObject[] altars))
            {
                return;
            }

            // Check if altar is in cooldown.
            int current = GameCore.AxeManCore.GetComponent<AltarCooldown>()
                .CurrentCooldown;
            int min = GameCore.AxeManCore.GetComponent<AltarCooldown>()
                .MinCooldown;
            if (current > min)
            {
                return;
            }

            // Grant altar effect to PC.
            SkillComponentTag skill
                = GameCore.AxeManCore.GetComponent<AltarEffect>()
                .GetEffect(altars[0].GetComponent<MetaInfo>().SubTag);
            int powerDuration
                = GameCore.AxeManCore.GetComponent<AltarEffect>()
                .GetPowerDuration(altars[0].GetComponent<MetaInfo>().SubTag);
            if (skill == SkillComponentTag.Life)
            {
                GetComponent<HP>().Add(powerDuration);
            }
            else
            {
                GetComponent<ActorStatus>().AddStatus(skill,
                    new EffectData(powerDuration, powerDuration));
            }

            // End PC's turn.
            ActionTag action = ActionTag.ActiveAltar;
            GetComponent<LocalManager>().TakenAction(action);
            GetComponent<LocalManager>().CheckingSchedule(action);
        }

        private void Start()
        {
            GetComponent<PCMove>().BlockingPCMovement
                += PCActiveAltar_BlockingPCMovement;
        }
    }
}
