﻿using AxeMan.DungeonObject;
using AxeMan.DungeonObject.PlayerInput;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class PCComponent : MonoBehaviour
    {
        private void PCComponent_AddingComponent(object sender,
            AddingComponentEventArgs e)
        {
            if (e.Data.GetComponent<MetaInfo>()?.STag != SubTag.PC)
            {
                return;
            }
            e.Data.AddComponent<PCInputManager>().enabled = false;
            e.Data.AddComponent<PCInputSwitcher>();

            e.Data.AddComponent<PCMove>();
            e.Data.AddComponent<PCStartEndTurn>();
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += PCComponent_AddingComponent;
        }
    }
}
