using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.Blueprint
{
    public class BpNewAltar : MonoBehaviour, IBlueprint
    {
        public IPrototype[] GetBlueprint()
        {
            Stack<IPrototype> blueprint = GetShieldAltar();
            blueprint.Push(GetHealthAltar());

            return blueprint.ToArray();
        }

        private IPrototype GetHealthAltar()
        {
            MainTag mt = MainTag.Building;
            SubTag st = SubTag.AltarHealth;
            int[] position = new int[] { 4, 4 };

            return new Prototype(mt, st, position);
        }

        private Stack<IPrototype> GetShieldAltar()
        {
            MainTag mt = MainTag.Building;
            // TODO: Get shield altar type from outside.
            SubTag st = SubTag.AltarHealth;
            int[][] position = new int[][]
            {
                new int[] { 2, 2 }, new int[] { 2, 6 },
                new int[] { 6, 2 }, new int[] { 6, 6 }
            };

            Stack<IPrototype> altar = new Stack<IPrototype>();
            foreach (int[] pos in position)
            {
                altar.Push(new Prototype(mt, st, pos));
            }
            return altar;
        }
    }
}
