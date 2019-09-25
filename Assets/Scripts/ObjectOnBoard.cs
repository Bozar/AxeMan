using AxeMan.DungeonObject;
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
            MainTag mainTag = go.GetComponent<MetaInfo>().MainTag;
            int[] position = go.GetComponent<MetaInfo>().Position;

            board[mainTag][position[0], position[1]] = go;
        }

        public void Remove(GameObject go)
        {
            MainTag mainTag = go.GetComponent<MetaInfo>().MainTag;
            int[] position = go.GetComponent<MetaInfo>().Position;

            if (board[mainTag][position[0], position[1]] == go)
            {
                board[mainTag][position[0], position[1]] = null;
            }
        }

        public GameObject[] Search(int x, int y)
        {
            Stack<GameObject> goStack = new Stack<GameObject>();

            foreach (MainTag mt in board.Keys)
            {
                if (board[mt][x, y] != null)
                {
                    goStack.Push(board[mt][x, y]);
                }
            }
            return goStack.ToArray();
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
