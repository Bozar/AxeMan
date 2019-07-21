using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintProgressBar : MonoBehaviour
    {
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
            int maxX = GetComponent<ProgressBar>().XCoordinate;
            int y = GetComponent<ProgressBar>().YCoordinate;

            for (int x = 0; x < maxX; x++)
            {
                bar.Push(new ProtoObject(MainTag.Marker, SubTag.ProgressBar,
                    new int[] { x, y }));
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
