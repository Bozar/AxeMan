using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using AxeMan.GameSystem.UserInterface;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.InitializeGameWorld
{
    public class InitializeStartScreen : MonoBehaviour
    {
        private bool dataLoaded;
        private bool hideCanvas;
        private bool showStart;

        public event EventHandler<EventArgs> LoadingGameData;

        public event EventHandler<EventArgs> LoadingSettingData;

        protected virtual void OnLoadingGameData(EventArgs e)
        {
            LoadingGameData?.Invoke(this, e);
        }

        protected virtual void OnLoadingSettingData(EventArgs e)
        {
            LoadingSettingData?.Invoke(this, e);
        }

        private void Awake()
        {
            dataLoaded = false;
        }

        private void EnterMainScreen()
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(
                CanvasTag.Canvas_Main, true);
            GetComponent<UIManager>().SwitchCanvasVisibility(
                CanvasTag.Canvas_Start, false);

            GetComponent<InitializeMainGame>().enabled = true;
            enabled = false;
        }

        private void EnterStartScreen()
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(
                CanvasTag.Canvas_Main, false);

            GameObject ui = GetComponent<SearchUI>().Search(
                CanvasTag.Canvas_Start, UITag.UIText);
            ui.GetComponent<Text>().text
                = "Start screen: Press Space to continue.";

            hideCanvas = true;
            enabled = false;
        }

        private void InitializeStartScreen_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if ((e.LeaveMode == GameModeTag.StartMode)
                && (e.EnterMode == GameModeTag.NormalMode))
            {
                EnterMainScreen();
            }
        }

        private void Start()
        {
            GetComponent<GameModeManager>().SwitchingGameMode
                += InitializeStartScreen_SwitchingGameMode;
        }

        private void Update()
        {
            if (!dataLoaded)
            {
                OnLoadingSettingData(EventArgs.Empty);
                OnLoadingGameData(EventArgs.Empty);
                showStart = GetComponent<SettingData>().GetBoolData(
                    SettingDataTag.ShowStartMenu);

                dataLoaded = true;
            }

            if (!showStart)
            {
                EnterMainScreen();
            }
            else if (!hideCanvas)
            {
                EnterStartScreen();
            }
        }
    }
}
