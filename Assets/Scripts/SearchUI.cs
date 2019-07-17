using AxeMan.GameSystem.GameDataTag;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.SearchGameObject
{
    public interface ISearchUI
    {
        GameObject Search(CanvasTag canvasTag, UITag uiTag);

        GameObject[] Search(CanvasTag canvasTag);

        GameObject SearchCanvas(CanvasTag canvasTag);
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
        public SearchingUIEventArgs(string canvasTag, Stack<GameObject> data)
        {
            CanvasTag = canvasTag;
            Data = data;
        }

        public string CanvasTag { get; }

        public Stack<GameObject> Data { get; }
    }

    public class SearchUI : MonoBehaviour, ISearchUI
    {
        public event EventHandler<SearchingCanvasEventArgs> SearchingCanvas;

        public event EventHandler<SearchingUIEventArgs> SearchingUI;

        public GameObject Search(CanvasTag canvasTag, UITag uiTag)
        {
            GameObject[] uiObjects = Search(canvasTag);
            string uiName = uiTag.ToString();

            foreach (GameObject go in uiObjects)
            {
                if (go.name == uiName)
                {
                    return go;
                }
            }
            return null;
        }

        public GameObject[] Search(CanvasTag canvasTag)
        {
            var ea = new SearchingUIEventArgs(canvasTag.ToString(),
                new Stack<GameObject>());
            OnSearchingUI(ea);

            return ea.Data.ToArray();
        }

        public GameObject SearchCanvas(CanvasTag canvasTag)
        {
            var ea = new SearchingCanvasEventArgs(canvasTag.ToString());
            OnSearchingCanvas(ea);

            return ea.Data;
        }

        public Text SearchText(CanvasTag canvasTag, UITag uiTag)
        {
            return Search(canvasTag, uiTag).GetComponent<Text>();
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
