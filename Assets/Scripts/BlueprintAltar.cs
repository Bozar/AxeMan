using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.PrototypeFactory
{
    public class BlueprintAltar : MonoBehaviour
    {
        private void BlueprintAltar_DrawingBlueprint(object sender,
            DrawingBlueprintEventArgs e)
        {
            if (e.BTag != BlueprintTag.Altar)
            {
                return;
            }

            Stack<IPrototype> blueprint = GetShieldAltar();
            blueprint.Push(GetLifeAltar());

            e.Data = blueprint.ToArray();
        }

        private IPrototype GetLifeAltar()
        {
            MainTag mt = MainTag.Building;
            SubTag st = SubTag.LifeAltar;
            int[] position = new int[] { 4, 4 };

            return new ProtoObject(mt, st, position);
        }

        private Stack<IPrototype> GetShieldAltar()
        {
            MainTag mt = MainTag.Building;
            SubTag st = SubTag.ShieldAltar;
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
