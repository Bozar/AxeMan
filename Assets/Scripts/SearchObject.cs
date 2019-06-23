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

    public class SearchingObjectEventArgs : EventArgs
    {
        public SearchingObjectEventArgs(SearchEventTag searchTag,
            Stack<GameObject> data)
        {
            SearchTag = searchTag;
            Data = data;
        }

        public Stack<GameObject> Data { get; }

        public MainTag MTag { get; set; }

        public int[] Position { get; set; }

        public SearchEventTag SearchTag { get; }

        public SubTag STag { get; set; }
    }

    public class SearchObject : MonoBehaviour, ISearchObject
    {
        public event EventHandler<SearchingObjectEventArgs> SearchingObject;

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
            var ea = GetSearchEventArg(new int[] { x, y });
            OnSearchingObject(ea);

            return ea.Data.ToArray();
        }

        public GameObject[] Search(MainTag mTag)
        {
            var ea = GetSearchEventArg(mTag);
            OnSearchingObject(ea);

            return ea.Data.ToArray();
        }

        public GameObject[] Search(SubTag sTag)
        {
            var ea = GetSearchEventArg(sTag);
            OnSearchingObject(ea);

            return ea.Data.ToArray();
        }

        protected virtual void OnSearchingObject(SearchingObjectEventArgs e)
        {
            SearchingObject?.Invoke(this, e);
        }

        private SearchingObjectEventArgs GetSearchEventArg(int[] position)
        {
            var ea = new SearchingObjectEventArgs(SearchEventTag.Position,
                new Stack<GameObject>())
            {
                Position = position
            };
            return ea;
        }

        private SearchingObjectEventArgs GetSearchEventArg(MainTag mTag)
        {
            var ea = new SearchingObjectEventArgs(SearchEventTag.MainTag,
                new Stack<GameObject>())
            {
                MTag = mTag
            };
            return ea;
        }

        private SearchingObjectEventArgs GetSearchEventArg(SubTag sTag)
        {
            var ea = new SearchingObjectEventArgs(SearchEventTag.SubTag,
                new Stack<GameObject>())
            {
                STag = sTag
            };
            return ea;
        }
    }
}
