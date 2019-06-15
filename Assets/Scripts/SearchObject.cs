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

        public GameObject[] Search(int x, int y)
        {
            var ea = new SearchingObjectEventArgs(
                SearchEventTag.Position, new Stack<GameObject>())
            {
                Position = new int[] { x, y }
            };
            OnSearchingObject(ea);

            return ea.Data.ToArray();
        }

        public GameObject[] Search(MainTag mTag)
        {
            throw new System.NotImplementedException();
        }

        public GameObject[] Search(SubTag sTag)
        {
            throw new System.NotImplementedException();
        }

        protected virtual void OnSearchingObject(SearchingObjectEventArgs e)
        {
            SearchingObject?.Invoke(this, e);
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
