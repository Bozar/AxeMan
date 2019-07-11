using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SearchGameObject;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface ITileOverlay
    {
        void TryHideTile(int x, int y);
    }

    public class TileOverlay : MonoBehaviour, ITileOverlay
    {
        private MainTag[] layer;

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
            if (!GetComponent<SearchObject>().Search(x, y,
                out GameObject[] search))
            {
                return;
            }
            GameObject probe = search[0];

            foreach (GameObject s in search)
            {
                SwitchRenderer(s, false);
                if (GetLayer(probe) < GetLayer(s))
                {
                    probe = s;
                }
            }
            SwitchRenderer(probe, true);
        }

        private void Awake()
        {
            layer = new MainTag[]
            {
                MainTag.INVALID, MainTag.Floor, MainTag.Trap, MainTag.Building,
                MainTag.Actor, MainTag.Marker,
            };
        }

        private int GetLayer(GameObject go)
        {
            int invalid = 0;

            if (go.GetComponent<MetaInfo>() == null)
            {
                return invalid;
            }
            return Array.IndexOf(layer, go.GetComponent<MetaInfo>().MTag);
        }

        private void SwitchRenderer(GameObject go, bool switchOn)
        {
            go.GetComponent<Renderer>().enabled = switchOn;
        }
    }
}
