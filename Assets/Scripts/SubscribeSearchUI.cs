using AxeMan.GameSystem;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.DungeonObject.SearchGameObject
{
    public class SubscribeSearchUI : MonoBehaviour
    {
        private void Start()
        {
            GameCore.AxeManCore.GetComponent<SearchUI>().SearchingUI
                += SubscribeSearchUI_SearchingUI;
            GameCore.AxeManCore.GetComponent<SearchUI>().SearchingCanvas
                += SubscribeSearchUI_SearchingCanvas;
        }

        private void SubscribeSearchUI_SearchingCanvas(object sender,
            SearchingCanvasEventArgs e)
        {
            if ((e.Data == null)
                && (GetComponentInParent<Canvas>().name == e.CanvasTag))
            {
                e.Data = GetComponentInParent<Canvas>().gameObject;
            }
        }

        private void SubscribeSearchUI_SearchingUI(object sender,
            SearchingUIEventArgs e)
        {
            if (GetComponentInParent<Canvas>().name == e.CanvasTag)
            {
                e.Data.Push(gameObject);
            }
        }
    }
}
