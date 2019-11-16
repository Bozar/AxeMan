using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.UserInterface
{
    public class Canvas_PCStatus : MonoBehaviour
    {
        private CanvasTag canvasTag;

        private void Awake()
        {
            canvasTag = CanvasTag.Canvas_PCStatus;
        }

        private void Canvas_PCStatus_EnteringLogMode(object sender, EventArgs e)
        {
            SwitchVisibility(false);
        }

        private void Canvas_PCStatus_LeavingLogMode(object sender, EventArgs e)
        {
            SwitchVisibility(true);
        }

        private void Start()
        {
            GetComponent<LogMode>().EnteringLogMode
                += Canvas_PCStatus_EnteringLogMode;
            GetComponent<LogMode>().LeavingLogMode
                += Canvas_PCStatus_LeavingLogMode;
        }

        private void SwitchVisibility(bool switchOn)
        {
            GetComponent<UIManager>().SwitchCanvasVisibility(canvasTag, switchOn);
        }
    }
}
