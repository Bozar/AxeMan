﻿using AxeMan.GameSystem;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.DungeonObject.GameEvent
{
    public class SubscribeSearchUIEvent : MonoBehaviour
    {
        private void Start()
        {
            GameCore.AxeManCore.GetComponent<SearchUI>().SearchingUI
                += SubscribeSearchUIEvent_SearchingUI;
        }

        private void SubscribeSearchUIEvent_SearchingUI(object sender,
            SearchingUIEventArgs e)
        {
            string canvas = GetComponentInParent<Canvas>().name;
            string ui = name;

            if ((e.CanvasTag == canvas) && (e.UITag == ui))
            {
                e.Data.Push(gameObject);
            }
        }
    }
}
