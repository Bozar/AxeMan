using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SearchGameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface ITileOverlay
    {
        GameObject[] GetSortedObjects(int x, int y);

        void TryHideTile(int x, int y);
    }

    public class TileOverlay : MonoBehaviour, ITileOverlay
    {
        private MainTag[] layer;

        public GameObject[] GetSortedObjects(int x, int y)
        {
            if (!GetComponent<SearchObject>().Search(x, y,
                out GameObject[] search))
            {
                return null;
            }

            IEnumerable<GameObject> sorted
                = from s in search
                  orderby GetLayer(s) descending
                  select s;
            return sorted.ToArray();
        }

        public void RefreshDungeonBoard()
        {
            for (int i = 0; i < GetComponent<DungeonBoard>().Width; i++)
            {
                for (int j = 0; j < GetComponent<DungeonBoard>().Height; j++)
                {
                    TryHideTile(i, j);
                }
            }
        }

        public void TryHideTile(int x, int y)
        {
            GameObject[] sorted = GetSortedObjects(x, y);
            if (sorted == null)
            {
                return;
            }

            SwitchRenderer(sorted[0], true);
            if (sorted.Length > 1)
            {
                for (int i = 1; i < sorted.Length; i++)
                {
                    SwitchRenderer(sorted[i], false);
                }
            }
        }

        public void TryHideTile(int[] position)
        {
            TryHideTile(position[0], position[1]);
        }

        private void Awake()
        {
            // NOTE: Two game objects of the same MainTag should not appear in
            // the same place. Objects are sorted from bottom to top.
            layer = new MainTag[]
            {
                MainTag.INVALID,
                MainTag.Floor,
                MainTag.Trap,
                MainTag.Building,
                MainTag.Actor,
                MainTag.Marker,
            };
        }

        private int GetLayer(GameObject go)
        {
            int invalid = 0;

            if (go.GetComponent<MetaInfo>() == null)
            {
                return invalid;
            }
            return Array.IndexOf(layer, go.GetComponent<MetaInfo>().MainTag);
        }

        private void SwitchRenderer(GameObject go, bool switchOn)
        {
            go.GetComponent<Renderer>().enabled = switchOn;
        }
    }
}
