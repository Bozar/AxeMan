using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SaveLoadGameFile;
using System;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface IActorData
    {
        int GetIntData(SubTag subTag, ActorDataTag actorData);

        string GetStringData(SubTag subTag, ActorDataTag actorData,
            LanguageTag language);
    }

    public class ActorData : MonoBehaviour, IActorData
    {
        private LanguageTag language;
        private XElement xmlFile;

        public int GetIntData(SubTag subTag, ActorDataTag actorData)
        {
            return (int)xmlFile
               .Element(subTag.ToString())
               .Element(actorData.ToString());
        }

        public string GetStringData(SubTag subTag, ActorDataTag actorData)
        {
            return GetStringData(subTag, actorData, language);
        }

        public string GetStringData(SubTag subTag, ActorDataTag actorData,
            LanguageTag language)
        {
            return (string)xmlFile
               .Element(subTag.ToString())
               .Element(actorData.ToString())
               .Element(language.ToString());
        }

        private void ActorData_LoadingGameData(object sender, EventArgs e)
        {
            // TODO: Read an actual data file.
            string file = "actorData.xml";
            string directory = "Build";

            xmlFile = GetComponent<SaveLoadXML>().Load(file, directory);

            // TODO: Retrieve language setting from another file.
            language = LanguageTag.English;
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().LoadingGameData
                += ActorData_LoadingGameData;
        }
    }
}
