using AxeMan.DungeonObject;
using AxeMan.GameSystem.ObjectFactory;
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

        public bool Search(int x, int y, MainTag mTag, out GameObject[] goArray)
        {
            goArray = Search(x, y);
            Stack<GameObject> goStack = new Stack<GameObject>();

            foreach (GameObject go in goArray)
            {
                if (go.GetComponent<MetaInfo>().MTag == mTag)
                {
                    goStack.Push(go);
                }
            }

            goArray = goStack.ToArray();
            return goArray.Length > 0;
        }

        public GameObject[] Search(int x, int y)
        {
            var ea = GetSearchEventArg(new int[] { x, y });
            OnSearchingObject(ea);

            return ea.Data.ToArray();
        }

        public GameObject[] Search(int[] position)
        {
            return Search(position[0], position[1]);
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

        private void SearchObject_CreatedObject(object sender,
            CreatedObjectEventArgs e)
        {
            SearchingObject += e.Data.GetComponent<LocalManager>().Search;
        }

        private void Start()
        {
            GetComponent<CreateObject>().CreatedObject
                += SearchObject_CreatedObject;
        }
    }
}
