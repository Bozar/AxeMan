using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_Start : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_Start;
        }

        private void Canvas_Start_LoadingGameData(object sender, EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);

            // TODO: Remove this later.
            SearchText(UITag.UIText).GetComponent<Text>().text
                = "Space: Continue; S: Build Skill";
        }

        private void Canvas_Start_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.LeaveMode == GameModeTag.StartMode)
            {
                SwitchVisibility(false);
            }
            else if (e.EnterMode == GameModeTag.StartMode)
            {
                SwitchVisibility(true);
            }
        }

        private Text SearchText(UITag uiTag)
        {
            return GetComponent<SearchUI>().SearchText(uiObjects, uiTag);
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += Canvas_Start_LoadingGameData;
            GetComponent<GameModeManager>().SwitchingGameMode
                += Canvas_Start_SwitchingGameMode;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
