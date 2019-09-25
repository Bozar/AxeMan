using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IObjectOnBoard
    {
        void Add(GameObject go);

        void Remove(GameObject go);

        GameObject[] Search(int x, int y);
    }

    public class ObjectOnBoard : MonoBehaviour, IObjectOnBoard
    {
        private Dictionary<MainTag, GameObject[,]> board;

        public void Add(GameObject go)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(GameObject go)
        {
            throw new System.NotImplementedException();
        }

        public GameObject[] Search(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        private void Awake()
        {
            board = new Dictionary<MainTag, GameObject[,]>();

            MainTag[] mainTags = new MainTag[]
            {
                MainTag.Actor,
                MainTag.Altar,
                MainTag.Floor,
                MainTag.Marker,
                MainTag.Trap,
            };
            int x = GetComponent<DungeonBoard>().Width;
            int y = GetComponent<DungeonBoard>().Height;

            foreach (MainTag mt in mainTags)
            {
                board[mt] = new GameObject[x, y];
            }
        }

        private void Start()
        {
        }
    }
}
