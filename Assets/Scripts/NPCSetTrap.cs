using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PrototypeFactory;
using System;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class NPCSetTrap : MonoBehaviour
    {
        private void NPCSetTrap_ChangingHP(object sender, ChangeHPEventArgs e)
        {
            if (e.IsAlive
                || (e.ID != GetComponent<MetaInfo>().ObjectID))
            {
                return;
            }
            SetTrap();
        }

        private void SetTrap()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            XElement trapData = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetXElementData(mainTag, subTag, ActorDataTag.SetTrap);
            Enum.TryParse((string)trapData, out SubTag trapTag);

            int[] position = GetComponent<MetaInfo>().Position;

            ProtoObject proto = new ProtoObject(MainTag.Trap, trapTag, position);
            GameCore.AxeManCore.GetComponent<CreateObject>().Create(proto);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PublishActorHP>().ChangingHP
                += NPCSetTrap_ChangingHP;
        }
    }
}
