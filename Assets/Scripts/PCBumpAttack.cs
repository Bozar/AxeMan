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
            actor.GetComponent<HP>().Subtract(bumpDamage);

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
