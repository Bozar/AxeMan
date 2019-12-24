using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.UserInterface;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.InitializeGameWorld
{
    public class InitializeStartScreen : MonoBehaviour
    {
        private bool dataLoaded;
        private SwitchGameModeEventArgs enterStart;

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
            enterStart = new SwitchGameModeEventArgs(
                GameModeTag.INVALID, GameModeTag.StartMode);
        }

        private void Update()
        {
            if (dataLoaded)
            {
                return;
            }
            OnLoadingSettingData(EventArgs.Empty);
            OnLoadingGameData(EventArgs.Empty);

            GetComponent<UIManager>().SwitchCanvasVisibility(
                CanvasTag.Canvas_Main, false);
            GetComponent<GameModeManager>().SwitchGameMode(enterStart);

            dataLoaded = true;
            enabled = false;
        }
    }
}
