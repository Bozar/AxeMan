using AxeMan.DungeonObject;
using AxeMan.GameSystem.ObjectFactory;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    // DungeonBoard is a data hub which records and updates every dungeon
    // object's position.
    public class DungeonBoard : MonoBehaviour, IDungeonBoard
    {
        private Dictionary<MainTag, IDungeonObject>[,] board;

        public int DungeonHeight { get; private set; }

        public int DungeonWidth { get; private set; }

        public bool AddObject(int x, int y, IDungeonObject ido, bool overwrite)
        {
            if (IndexOutOfRange(x, y))
            {
                return false;
            }
            else if (!overwrite && board[x, y].ContainsKey(ido.MTag))
            {
                return false;
            }
            board[x, y][ido.MTag] = ido;
            return true;
        }

        public bool ExistObject(int x, int y, MainTag mtag)
        {
            if (IndexOutOfRange(x, y))
            {
                return false;
            }
            return board[x, y].ContainsKey(mtag);
        }

        public IDungeonObject GetObject(int x, int y, MainTag mtag)
        {
            if (IndexOutOfRange(x, y))
            {
                return null;
            }
            else if (board[x, y].TryGetValue(mtag, out IDungeonObject ido))
            {
                return ido;
            }
            return null;
        }

        public IDungeonObject RemoveObject(int x, int y, MainTag mtag)
        {
            IDungeonObject ido = GetObject(x, y, mtag);
            board[x, y].Remove(mtag);

            return ido;
        }

        private void Awake()
        {
            DungeonWidth = 9;
            DungeonHeight = 9;

            board = new Dictionary<MainTag, IDungeonObject>[
                DungeonWidth, DungeonHeight];
            for (int i = 0; i < DungeonWidth; i++)
            {
                for (int j = 0; j < DungeonHeight; j++)
                {
                    board[i, j] = new Dictionary<MainTag, IDungeonObject>();
                }
            }
        }

        private void DungeonBoard_CreatedObject(object sender,
            CreatedObjectEventArgs e)
        {
            GameObject go = e.Data;
            int[] pos = GetComponent<ConvertCoordinate>().Convert(
                go.transform.position);
            IDungeonObject ido = new DungeonObject(
                go.GetComponent<MetaInfo>().MTag, go);

            AddObject(pos[0], pos[1], ido, false);
        }

        private bool IndexOutOfRange(int x, int y)
        {
            return (x < 0)
                || (x >= DungeonWidth)
                || (y < 0)
                || (y >= DungeonHeight);
        }

        private void Start()
        {
            GetComponent<ObjectFactoryCore>().CreatedObject
                += DungeonBoard_CreatedObject;
        }
    }
}
