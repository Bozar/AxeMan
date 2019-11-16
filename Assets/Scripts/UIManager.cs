using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SearchGameObject;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.UserInterface
{
    public interface IUIManager
    {
        void SwitchCanvasVisibility(CanvasTag cTag, bool switchOn);
    }

    public class UIManager : MonoBehaviour, IUIManager
    {
        private Dictionary<CanvasTag, CanvasGroup> canvasDict;

        public void SwitchCanvasVisibility(CanvasTag cTag, bool switchOn)
        {
            if (!canvasDict.ContainsKey(cTag))
            {
                canvasDict[cTag] = GetComponent<SearchUI>().SearchCanvas(cTag)
                    .GetComponent<CanvasGroup>();
            }
            canvasDict[cTag].alpha = switchOn ? 1 : 0;
        }

        private void Awake()
        {
            canvasDict = new Dictionary<CanvasTag, CanvasGroup>();
        }
    }
}
