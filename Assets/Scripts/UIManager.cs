using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.GameSystem.UserInterface
{
    public interface IUIManager
    {
        void SwitchCanvas(CanvasTag cTag, bool switchOn);
    }

    public class UIManager : MonoBehaviour, IUIManager
    {
        public void SwitchCanvas(CanvasTag cTag, bool switchOn)
        {
            GetComponent<SearchUI>().SearchCanvas(cTag)
                .GetComponent<CanvasGroup>().alpha
                = switchOn ? 1 : 0;
        }
    }
}
