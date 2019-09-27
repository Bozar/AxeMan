using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCBumpAttack : MonoBehaviour
    {
        private int bumpDamage;

        private int GetDamage(ActorStatus actor)
        {
            int extraDamage = 0;

            if (actor.HasStatus(SkillComponentTag.AirFlaw,
                out EffectData effectData))
            {
                extraDamage = effectData.Power;
            }
            return bumpDamage + extraDamage;
        }

        private void PCBumpAttack_BlockingPCMovement(object sender,
            BlockPCMovementEventArgs e)
        {
            int x = e.TargetPosition[0];
            int y = e.TargetPosition[1];
            if (!GameCore.AxeManCore.GetComponent<SearchObject>().Search(
                x, y, MainTag.Actor, out GameObject[] actors))
            {
                return;
            }

            GameObject actor = actors[0];
            int damage = GetDamage(actor.GetComponent<ActorStatus>());
            actor.GetComponent<HP>().Subtract(damage);

            GetComponent<LocalManager>().CheckingSchedule(ActionTag.BumpAttack);
        }

        private void PCBumpAttack_CreatedWorld(object sender, EventArgs e)
        {
            bumpDamage = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(MainTag.Actor, SubTag.PC, ActorDataTag.BumpDamage);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PublishPosition>()
                .BlockingPCMovement += PCBumpAttack_BlockingPCMovement;
            GameCore.AxeManCore.GetComponent<InitializeMainGame>()
                .CreatedWorld += PCBumpAttack_CreatedWorld;
        }
    }
}
