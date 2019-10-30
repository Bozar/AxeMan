using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SchedulingSystem;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class AltarColor : MonoBehaviour
    {
        private GameObject[] altars;

        private void AltarColor_CreatedWorld(object sender, EventArgs e)
        {
            altars = GetComponent<SearchObject>().Search(SubTag.LifeAltar);
        }

        private void AltarColor_StartedTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (e.SubTag != SubTag.PC)
            {
                return;
            }

            if (GetComponent<AltarCooldown>().CurrentCooldown
                > GetComponent<AltarCooldown>().MinCooldown)
            {
                SetAltarColor(ColorTag.Grey);
            }
            else
            {
                SetAltarColor(ColorTag.White);
            }
        }

        private void SetAltarColor(ColorTag colorTag)
        {
            foreach (GameObject go in altars)
            {
                go.GetComponent<SpriteRenderer>().color
                    = GetComponent<ColorData>().GetRGBAColor(colorTag);
            }
        }

        private void Start()
        {
            GetComponent<TurnManager>().StartedTurn
                += AltarColor_StartedTurn;
            GetComponent<InitializeMainGame>().CreatedWorld
                += AltarColor_CreatedWorld;
        }
    }
}
