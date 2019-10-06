using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintAltar : MonoBehaviour
    {
        private int[][] altarPositions;

        private void Awake()
        {
            altarPositions = new int[][]
            {
                new int[] { 2, 2 },
                new int[] { 2, 6 },
                new int[] { 6, 2 },
                new int[] { 6, 6 },
            };
        }

        private void BlueprintAltar_DrawingBlueprint(object sender,
            DrawingBlueprintEventArgs e)
        {
            if (e.BlueprintTag != BlueprintTag.Altar)
            {
                return;
            }

            Stack<IPrototype> blueprint = new Stack<IPrototype>();
            foreach (int[] ap in altarPositions)
            {
                blueprint.Push(GetLifeAltar(ap));
            }
            e.Data = blueprint.ToArray();
        }

        private IPrototype GetLifeAltar(int[] position)
        {
            MainTag mt = MainTag.Altar;
            SubTag st = SubTag.LifeAltar;

            return new ProtoObject(mt, st, position);
        }

        private IPrototype GetLifeAltar()
        {
            MainTag mt = MainTag.Altar;
            SubTag st = SubTag.LifeAltar;
            int[] position = new int[] { 4, 4 };

            return new ProtoObject(mt, st, position);
        }

        private Stack<IPrototype> GetShieldAltar()
        {
            MainTag mt = MainTag.Altar;
            SubTag st = SubTag.EarthAltar;
            int[][] position = new int[][]
            {
                new int[] { 2, 2 }, new int[] { 2, 6 },
                new int[] { 6, 2 }, new int[] { 6, 6 }
            };

            Stack<IPrototype> altar = new Stack<IPrototype>();
            foreach (int[] pos in position)
            {
                altar.Push(new ProtoObject(mt, st, pos));
            }
            return altar;
        }

        private void Start()
        {
            GetComponent<Blueprint>().DrawingBlueprint
                += BlueprintAltar_DrawingBlueprint;
        }
    }
}
