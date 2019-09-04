using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SaveLoadGameFile;
using System;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface IUILabelData
    {
        string GetStringData(UILabelDataTag uiLabelData);
    }

    public class UILabelData : MonoBehaviour, IUILabelData
    {
        private LanguageTag defaultLanguage;
        private string error;
        private LanguageTag userLanguage;
        private XElement xmlFile;

        public string GetStringData(UILabelDataTag uiLabelData)
        {
            if (TryGetData(uiLabelData, userLanguage, out XElement data)
                || TryGetData(uiLabelData, defaultLanguage, out data))
            {
                return (string)data;
            }
            return error;
        }

        private void Awake()
        {
            error = "INVALID";
            defaultLanguage = LanguageTag.English;
            userLanguage = LanguageTag.English;
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += UILabelData_LoadingGameData;
        }

        private bool TryGetData(UILabelDataTag uiLabelData, LanguageTag language,
            out XElement data)
        {
            data = xmlFile
                .Element(uiLabelData.ToString())
                .Element(language.ToString());
            return data != null;
        }

        private void UILabelData_LoadingGameData(object sender, EventArgs e)
        {
            string file = "uiLabelData.xml";
            string directory = "Data";

            xmlFile = GetComponent<SaveLoadXML>().Load(file, directory);
        }
    }
}
