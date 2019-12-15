using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintProgressBar : MonoBehaviour
    {
        private int barX;
        private int barY;

        private void Awake()
        {
            barX = 5;
            barY = 9;
        }

        private void BlueprintProgressBar_DrawingBlueprint(object sender,
            DrawingBlueprintEventArgs e)
        {
            if (e.BlueprintTag != BlueprintTag.ProgressBar)
            {
                return;
            }
            e.Data = GetProgressBar();
        }

        private IPrototype[] GetProgressBar()
        {
            Stack<IPrototype> bar = new Stack<IPrototype>();

            for (int x = 0; x < barX; x++)
            {
                bar.Push(new ProtoObject(MainTag.Marker, SubTag.ProgressBar,
                    new int[] { x, barY }));
            }
            return bar.ToArray();
        }

        private void Start()
        {
            GetComponent<Blueprint>().DrawingBlueprint
                += BlueprintProgressBar_DrawingBlueprint;
        }
    }
}
