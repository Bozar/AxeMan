using AxeMan.GameSystem.GameDataTag;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem.SearchGameObject
{
    public interface ISearchUI
    {
        GameObject[] Search(CanvasTag cTag, UITag uTag);
    }

    public class SearchingUIEventArgs : EventArgs
    {
        public SearchingUIEventArgs(string canvasTag, string uiTag,
            Stack<GameObject> data)
        {
            CanvasTag = canvasTag;
            UITag = uiTag;
            Data = data;
        }

        public string CanvasTag { get; }

        public Stack<GameObject> Data { get; }

        public string UITag { get; }
    }

    public class SearchUI : MonoBehaviour, ISearchUI
    {
        public event EventHandler<SearchingUIEventArgs> SearchingUI;

        public GameObject[] Search(CanvasTag cTag, UITag uTag)
        {
            Stack<GameObject> result = new Stack<GameObject>();
            OnSearchingUI(new SearchingUIEventArgs(
                cTag.ToString(), uTag.ToString(), result));

            return result.ToArray();
        }

        protected virtual void OnSearchingUI(SearchingUIEventArgs e)
        {
            SearchingUI?.Invoke(this, e);
        }
    }
}
