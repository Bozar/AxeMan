using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_BuildSkill : MonoBehaviour
    {
        private CanvasTag canvasTag;
        private GameObject[] uiObjects;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_BuildSkill;
        }

        private void Canvas_BuildSkill_LoadingGameData(object sender,
            EventArgs e)
        {
            uiObjects = GetComponent<SearchUI>().Search(canvasTag);
            SwitchVisibility(false);

            // TODO: Remove this later.
            SearchText(UITag.UIText).GetComponent<Text>().text = "Build skill";
        }

        private void Canvas_BuildSkill_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if (e.LeaveMode == GameModeTag.BuildSkillMode)
            {
                SwitchVisibility(false);
            }
            else if (e.EnterMode == GameModeTag.BuildSkillMode)
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
                += Canvas_BuildSkill_LoadingGameData;
            GetComponent<GameModeManager>().SwitchingGameMode
                += Canvas_BuildSkill_SwitchingGameMode;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
