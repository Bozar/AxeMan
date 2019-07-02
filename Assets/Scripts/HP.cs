using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface IHP
    {
        int Current { get; }

        int Max { get; }

        int Add(int hp);

        int Subtract(int hp);
    }

    public class HP : MonoBehaviour, IHP
    {
        public int Current { get; }

        public int Max { get; }

        public int Add(int hp)
        {
            throw new System.NotImplementedException();
        }

        public int Subtract(int hp)
        {
            throw new System.NotImplementedException();
        }
    }
}
