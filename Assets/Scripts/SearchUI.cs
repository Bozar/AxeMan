using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.SearchGameObject
{
    public interface ISearchUI
    {
        GameObject Search(CanvasTag cTag, UITag uTag);

        GameObject SearchCanvas(CanvasTag cTag);
    }

    public class SearchingCanvasEventArgs : EventArgs
    {
        public SearchingCanvasEventArgs(string canvasTag)
        {
            CanvasTag = canvasTag;
        }

        public string CanvasTag { get; }

        public GameObject Data { get; set; }
    }

    public class SearchingUIEventArgs : EventArgs
    {
        public SearchingUIEventArgs(string canvasTag, string uiTag)
        {
            CanvasTag = canvasTag;
            UITag = uiTag;
        }

        public string CanvasTag { get; }

        public GameObject Data { get; set; }

        public string UITag { get; }
    }

    public class SearchUI : MonoBehaviour, ISearchUI
    {
        public event EventHandler<SearchingCanvasEventArgs> SearchingCanvas;

        public event EventHandler<SearchingUIEventArgs> SearchingUI;

        public GameObject Search(CanvasTag cTag, UITag uTag)
        {
            var ea = new SearchingUIEventArgs(cTag.ToString(), uTag.ToString());
            OnSearchingUI(ea);

            return ea.Data;
        }

        public GameObject SearchCanvas(CanvasTag cTag)
        {
            var ea = new SearchingCanvasEventArgs(cTag.ToString());
            OnSearchingCanvas(ea);

            return ea.Data;
        }

        public Text SearchText(CanvasTag cTag, UITag uTag)
        {
            return Search(cTag, uTag).GetComponent<Text>();
        }

        protected virtual void OnSearchingCanvas(SearchingCanvasEventArgs e)
        {
            SearchingCanvas?.Invoke(this, e);
        }

        protected virtual void OnSearchingUI(SearchingUIEventArgs e)
        {
            SearchingUI?.Invoke(this, e);
        }
    }
}
