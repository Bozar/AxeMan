using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class MarkerPosition : MonoBehaviour
    {
        private void MarkerPosition_EnteringAimMode(object sender,
            EnterAimModeEventArgs e)
        {
            if (e.SubTag == SubTag.AimMarker)
            {
                return;
            }

            GameObject pc = GameCore.AxeManCore.GetComponent<SearchObject>()
                .Search(SubTag.PC)[0];
            int[] position = pc.GetComponent<MetaInfo>().Position;

            GetComponent<LocalManager>().SetPosition(position);
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(position);
        }

        private void MarkerPosition_LeavingAimMode(object sender, EventArgs e)
        {
            int invalid = -999;
            int[] position = GetComponent<MetaInfo>().Position;

            GetComponent<LocalManager>().SetPosition(
                new int[] { invalid, invalid });
            GameCore.AxeManCore.GetComponent<TileOverlay>().TryHideTile(position);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<AimMode>().EnteringAimMode
                += MarkerPosition_EnteringAimMode;
            GameCore.AxeManCore.GetComponent<AimMode>().LeavingAimMode
                += MarkerPosition_LeavingAimMode;
        }
    }
}
