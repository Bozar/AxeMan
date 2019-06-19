using AxeMan.DungeonObject;
using AxeMan.GameSystem.ObjectFactory;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface ITileOverlay
    {
        void TryHideTile(GameObject go);
    }

    public class TileOverlay : MonoBehaviour, ITileOverlay
    {
        private MainTag[] layer;

        public void TryHideTile(GameObject go)
        {
            int tryHideGo = GetIndex(go);
            int[] position = GetComponent<ConvertCoordinate>().Convert(
                go.transform.position);
            GameObject[] search = GetComponent<SearchObject>().Search(position);

            foreach (GameObject s in search)
            {
                SwitchRenderer(s, true);
                if (tryHideGo < GetIndex(s))
                {
                    SwitchRenderer(go, false);
                }
                else if (tryHideGo > GetIndex(s))
                {
                    SwitchRenderer(s, false);
                }
            }
        }

        private void Awake()
        {
            layer = new MainTag[]
            {
                MainTag.INVALID,
                MainTag.Floor, MainTag.Trap, MainTag.Building, MainTag.Actor
            };
        }

        private int GetIndex(GameObject go)
        {
            int invalidIndex = -1;

            if (go.GetComponent<MetaInfo>() == null)
            {
                return invalidIndex;
            }
            return Array.IndexOf(layer, go.GetComponent<MetaInfo>().MTag);
        }

        private void Start()
        {
            GetComponent<CreateObject>().CreatedObject
                += TileOverlay_CreatedObject;
        }

        private void SwitchRenderer(GameObject go, bool switchOn)
        {
            go.GetComponent<Renderer>().enabled = switchOn;
        }

        private void TileOverlay_CreatedObject(object sender,
            CreatedObjectEventArgs e)
        {
            TryHideTile(e.Data);
        }
    }
}
