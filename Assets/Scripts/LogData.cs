using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SaveLoadGameFile;
using System;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface ILogData
    {
        string GetStringData(LogMessage logMessage);
    }

    public class LogData : MonoBehaviour, ILogData
    {
        private LanguageTag defaultLanguage;
        private string errorMessage;
        private XElement logDataFile;
        private string replaceActor;
        private string replaceTrap;
        private LanguageTag userLanguage;

        public string GetStringData(LogMessage logMessage)
        {
            string message;

            if (TryGetData(logMessage, userLanguage, out XElement xElement)
                || TryGetData(logMessage, defaultLanguage, out xElement))
            {
                message = (string)xElement;

                message = TryReplacePlaceholder(message, replaceActor,
                    MainTag.Actor, logMessage.ActorTag);

                message = TryReplacePlaceholder(message, replaceTrap,
                   MainTag.Trap, logMessage.TrapTag);
            }
            else
            {
                message = errorMessage;
            }
            return message;
        }

        private void Awake()
        {
            defaultLanguage = LanguageTag.English;
            errorMessage = "INVALID";
            replaceActor = "%ACTOR%";
            replaceTrap = "%TRAP%";
        }

        private void LoadLogData()
        {
            string file = "logData.xml";
            string directory = "Data";

            logDataFile = GetComponent<SaveLoadXML>().Load(file, directory);
        }

        private void LoadUserLanguage()
        {
            string language = GetComponent<SettingData>().GetStringData(
               SettingDataTag.Language);
            Enum.TryParse(language, out userLanguage);
        }

        private void LogData_LoadingGameData(object sender, EventArgs e)
        {
            LoadUserLanguage();
            LoadLogData();
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += LogData_LoadingGameData;
        }

        private bool TryGetData(LogMessage logMessage, LanguageTag languageTag,
            out XElement xElement)
        {
            xElement = logDataFile
                ?.Element(logMessage.LogCategoryTag.ToString())
                ?.Element(logMessage.LogMessageTag.ToString())
                ?.Element(languageTag.ToString());

            return xElement != null;
        }

        private string TryReplacePlaceholder(string source, string placeholder,
            MainTag mainTag, SubTag subTag)
        {
            string newText;

            if (subTag != SubTag.INVALID)
            {
                newText = GetComponent<ActorData>().GetStringData(
                    mainTag, subTag, ActorDataTag.Name);
                source = source.Replace(placeholder, newText);
            }
            return source;
        }
    }
}
