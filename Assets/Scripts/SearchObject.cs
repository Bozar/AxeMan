using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.SearchGameObject
{
    public interface ISearchObject
    {
        GameObject[] Search(int x, int y);

        GameObject[] Search(MainTag mainTag);

        GameObject[] Search(SubTag subTag);
    }

    public class SearchingMainTagEventArgs : EventArgs
    {
        public SearchingMainTagEventArgs(MainTag mainTag, Stack<GameObject> data)
        {
            MainTag = mainTag;
            Data = data;
        }

        public Stack<GameObject> Data { get; }

        public MainTag MainTag { get; }
    }

    public class SearchingPositionEventArgs : EventArgs
    {
        public SearchingPositionEventArgs(int[] position, Stack<GameObject> data)
        {
            Position = position;
            Data = data;
        }

        public Stack<GameObject> Data { get; }

        public int[] Position { get; }
    }

    public class SearchingSubTagEventArgs : EventArgs
    {
        public SearchingSubTagEventArgs(SubTag subTag, Stack<GameObject> data)
        {
            SubTag = subTag;
            Data = data;
        }

        public Stack<GameObject> Data { get; }

        public SubTag SubTag { get; }
    }

    public class SearchObject : MonoBehaviour, ISearchObject
    {
        public event EventHandler<SearchingMainTagEventArgs> SearchingMainTag;

        public event EventHandler<SearchingPositionEventArgs> SearchingPosition;

        public event EventHandler<SearchingSubTagEventArgs> SearchingSubTag;

        public bool Search(int x, int y, MainTag mainTag, out GameObject[] result)
        {
            result = Filter(Search(x, y), mainTag);
            return result.Length > 0;
        }

        public bool Search(int x, int y, SubTag subTag, out GameObject[] result)
        {
            result = Filter(Search(x, y), subTag);
            return result.Length > 0;
        }

        public bool Search(int x, int y, out GameObject[] result)
        {
            result = Search(x, y);
            return result.Length > 0;
        }

        public GameObject[] Search(int x, int y)
        {
            int[] pos = new int[] { x, y };
            Stack<GameObject> data = new Stack<GameObject>();
            var ea = new SearchingPositionEventArgs(pos, data);

            OnSearchingPosition(ea);
            return ea.Data.ToArray();
        }

        public GameObject[] Search(MainTag mainTag)
        {
            Stack<GameObject> data = new Stack<GameObject>();
            var ea = new SearchingMainTagEventArgs(mainTag, data);

            OnSearchingMainTag(ea);
            return ea.Data.ToArray();
        }

        public GameObject[] Search(SubTag subTag)
        {
            Stack<GameObject> data = new Stack<GameObject>();
            var ea = new SearchingSubTagEventArgs(subTag, data);

            OnSearchingSubTag(ea);
            return ea.Data.ToArray();
        }

        protected virtual void OnSearchingMainTag(SearchingMainTagEventArgs e)
        {
            SearchingMainTag?.Invoke(this, e);
        }

        protected virtual void OnSearchingPosition(SearchingPositionEventArgs e)
        {
            SearchingPosition?.Invoke(this, e);
        }

        protected virtual void OnSearchingSubTag(SearchingSubTagEventArgs e)
        {
            SearchingSubTag?.Invoke(this, e);
        }

        private GameObject[] Filter(GameObject[] source, MainTag mainTag)
        {
            Stack<GameObject> result = new Stack<GameObject>();

            foreach (GameObject go in source)
            {
                if (go.GetComponent<MetaInfo>().MainTag == mainTag)
                {
                    result.Push(go);
                }
            }
            return result.ToArray();
        }

        private GameObject[] Filter(GameObject[] source, SubTag subTag)
        {
            Stack<GameObject> result = new Stack<GameObject>();

            foreach (GameObject go in source)
            {
                if (go.GetComponent<MetaInfo>().SubTag == subTag)
                {
                    result.Push(go);
                }
            }
            return result.ToArray();
        }
    }
}
