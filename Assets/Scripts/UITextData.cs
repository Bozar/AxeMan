using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SaveLoadGameFile;
using System;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface IUITextData
    {
        string GetStringData(UITextDataTag uiTextDataTag);
    }

    public class UITextData : MonoBehaviour, IUITextData
    {
        private LanguageTag defaultLanguage;
        private string error;
        private LanguageTag userLanguage;
        private XElement xmlFile;

        public string GetStringData(UITextDataTag uiTextDataTag)
        {
            if (TryGetData(uiTextDataTag, userLanguage, out XElement data)
                || TryGetData(uiTextDataTag, defaultLanguage, out data))
            {
                return (string)data;
            }
            return error;
        }

        private void Awake()
        {
            error = "INVALID";
            defaultLanguage = LanguageTag.English;
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += UILabelData_LoadingGameData;
        }

        private bool TryGetData(UITextDataTag uiLabelData, LanguageTag language,
            out XElement data)
        {
            data = xmlFile
                .Element(uiLabelData.ToString())
                .Element(language.ToString());
            return data != null;
        }

        private void UILabelData_LoadingGameData(object sender, EventArgs e)
        {
            string file = "uiTextData.xml";
            string directory = "Data";
            xmlFile = GetComponent<SaveLoadXML>().Load(file, directory);

            string language = GetComponent<SettingData>().GetStringData(
                SettingDataTag.Language);
            Enum.TryParse(language, out userLanguage);
        }
    }
}
