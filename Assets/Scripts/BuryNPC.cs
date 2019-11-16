using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PrototypeFactory;
using AxeMan.GameSystem.SchedulingSystem;
using AxeMan.GameSystem.SearchGameObject;
using System;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class BuryNPC : MonoBehaviour
    {
        private void BuryNPC_ChangedHP(object sender, ChangeHPEventArgs e)
        {
            if (e.IsAlive || (e.SubTag == SubTag.PC))
            {
                return;
            }
            if (!GetComponent<SearchObject>().Search(e.ID,
                out GameObject[] actors))
            {
                return;
            }

            GameObject actor = actors[0];
            bool isCurrentActor = (actor == GetComponent<Schedule>().Current);
            MainTag mainTag = actor.GetComponent<MetaInfo>().MainTag;
            SubTag subTag = actor.GetComponent<MetaInfo>().SubTag;
            int[] position = actor.GetComponent<MetaInfo>().Position;

            //SetTrap(mainTag, subTag, position);
            actor.GetComponent<LocalManager>().Remove();
            GetComponent<TileOverlay>().TryHideTile(position);

            // Call StartTurn() manually only when an active actor kills
            // himself. Otherwise StartTurn() will be called implicitly.
            if (isCurrentActor)
            {
                GetComponent<TurnManager>().StartTurn();
            }
        }

        private void SetTrap(MainTag mainTag, SubTag subTag, int[] position)
        {
            XElement trapData = GetComponent<ActorData>()
                .GetXElementData(mainTag, subTag, ActorDataTag.SetTrap);
            Enum.TryParse((string)trapData, out SubTag trapTag);

            ProtoObject proto = new ProtoObject(MainTag.Trap, trapTag, position);
            GetComponent<CreateObject>().Create(proto);
        }

        private void Start()
        {
            GetComponent<PublishActorHP>().ChangedHP += BuryNPC_ChangedHP;
        }
    }
}
