using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PrototypeFactory;
using AxeMan.GameSystem.SchedulingSystem;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class ProgressBar : MonoBehaviour
    {
        private GameObject[] bars;
        private int currentX;
        private int maxX;
        private int minX;

        private void Awake()
        {
            minX = 1;
            maxX = 5;
            currentX = maxX;
        }

        private int GetXCoordinate(int current, int min, int max)
        {
            int result = ++current;
            if (result > max)
            {
                result = min;
            }
            return result;
        }

        private void ProgressBar_CreatedWorld(object sender, EventArgs e)
        {
            IPrototype[] protoBar = GetComponent<Blueprint>().GetBlueprint(
                BlueprintTag.ProgressBar);
            bars = GetComponent<CreateObject>().Create(protoBar);
        }

        private void ProgressBar_EndingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (e.SubTag != SubTag.PC)
            {
                return;
            }

            SpriteRenderer sr;
            currentX = GetXCoordinate(currentX, minX, maxX);
            for (int i = 0; i < bars.Length; i++)
            {
                sr = bars[i].GetComponent<SpriteRenderer>();
                if (i < currentX)
                {
                    GetComponent<ColorManager>().SetColor(sr, ColorTag.Grey);
                }
                else
                {
                    GetComponent<ColorManager>().SetColor(sr, ColorTag.Black);
                }
            }
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().CreatedWorld
                += ProgressBar_CreatedWorld;
            GetComponent<TurnManager>().EndingTurn
                += ProgressBar_EndingTurn;
        }
    }
}
