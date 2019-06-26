using AxeMan.Actor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface ISearchObject
    {
        GameObject[] Search(int x, int y);

        GameObject[] Search(MainTag mTag);

        GameObject[] Search(SubTag sTag);
    }

    public class SearchingMainTagEventArgs : EventArgs
    {
        public SearchingMainTagEventArgs(MainTag mTag, Stack<GameObject> data)
        {
            MTag = mTag;
            Data = data;
        }

        public Stack<GameObject> Data { get; }

        public MainTag MTag { get; }
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
        public SearchingSubTagEventArgs(SubTag sTag, Stack<GameObject> data)
        {
            STag = sTag;
            Data = data;
        }

        public Stack<GameObject> Data { get; }

        public SubTag STag { get; }
    }

    public class SearchObject : MonoBehaviour, ISearchObject
    {
        public event EventHandler<SearchingMainTagEventArgs> SearchingMainTag;

        public event EventHandler<SearchingPositionEventArgs> SearchingPosition;

        public event EventHandler<SearchingSubTagEventArgs> SearchingSubTag;

        public bool Search(int x, int y, MainTag mTag, out GameObject[] result)
        {
            result = Search(x, y);
            Stack<GameObject> filter = new Stack<GameObject>();

            foreach (GameObject go in result)
            {
                if (go.GetComponent<MetaInfo>().MTag == mTag)
                {
                    filter.Push(go);
                }
            }

            result = filter.ToArray();
            return result.Length > 0;
        }

        public bool Search(int x, int y, SubTag sTag, out GameObject[] result)
        {
            result = Search(x, y);
            Stack<GameObject> filter = new Stack<GameObject>();

            foreach (GameObject go in result)
            {
                if (go.GetComponent<MetaInfo>().STag == sTag)
                {
                    filter.Push(go);
                }
            }

            result = filter.ToArray();
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

        public GameObject[] Search(MainTag mTag)
        {
            Stack<GameObject> data = new Stack<GameObject>();
            var ea = new SearchingMainTagEventArgs(mTag, data);

            OnSearchingMainTag(ea);
            return ea.Data.ToArray();
        }

        public GameObject[] Search(SubTag sTag)
        {
            Stack<GameObject> data = new Stack<GameObject>();
            var ea = new SearchingSubTagEventArgs(sTag, data);

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
    }
}
