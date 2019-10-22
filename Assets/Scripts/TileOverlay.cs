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

        GameObject[] GetSortedObjects(GameObject[] gameObjects);

        void TryHideTile(int x, int y);

        void TryHideTile(GameObject[] gameObjects);
    }

    public class TileOverlay : MonoBehaviour, ITileOverlay
    {
        private MainTag[] layer;

        public GameObject[] GetSortedObjects(int x, int y)
        {
            if (TrySearchObjects(x, y, out GameObject[] search))
            {
                return GetSortedObjects(search);
            }
            return null;
        }

        public GameObject[] GetSortedObjects(GameObject[] gameObjects)
        {
            IEnumerable<GameObject> sorted
               = from go in gameObjects
                 orderby GetLayer(go) descending
                 select go;

            return sorted.ToArray();
        }

        public void TryHideTile(int x, int y)
        {
            if (TrySearchObjects(x, y, out GameObject[] search))
            {
                TryHideTile(search);
            }
        }

        public void TryHideTile(int[] position)
        {
            TryHideTile(position[0], position[1]);
        }

        public void TryHideTile(GameObject[] gameObjects)
        {
            GameObject[] sorted = GetSortedObjects(gameObjects);

            SwitchRenderer(sorted[0], true);
            if (sorted.Length > 1)
            {
                for (int i = 1; i < sorted.Length; i++)
                {
                    SwitchRenderer(sorted[i], false);
                }
            }
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
                MainTag.Altar,
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

        private bool TrySearchObjects(int x, int y, out GameObject[] gameObjects)
        {
            if (!GetComponent<SearchObject>().Search(x, y,
                out GameObject[] search))
            {
                gameObjects = null;
                return false;
            }
            gameObjects = search;
            return true;
        }
    }
}
