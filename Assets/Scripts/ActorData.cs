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

        string GetStringData(SubTag subTag, ActorDataTag actorData);
    }

    public class ActorData : MonoBehaviour, IActorData
    {
        private SubTag defaultActor;
        private LanguageTag defaultLanguage;
        private LanguageTag language;
        private XElement xmlFile;

        public int GetIntData(SubTag subTag, ActorDataTag actorData)
        {
            XElement data = TryGetData(subTag, actorData);

            if (data == null)
            {
                return (int)TryGetData(defaultActor, actorData);
            }
            return (int)data;
        }

        public string GetStringData(SubTag subTag, ActorDataTag actorData)
        {
            XElement data = TryGetData(subTag, actorData, language);

            if (data == null)
            {
                return (string)TryGetData(subTag, actorData, defaultLanguage);
            }
            return (string)data;
        }

        private void ActorData_LoadingGameData(object sender, EventArgs e)
        {
            // TODO: Read an actual data file.
            string file = "actorData.xml";
            string directory = "Data";

            xmlFile = GetComponent<SaveLoadXML>().Load(file, directory);

            // TODO: Retrieve language setting from another file.
            language = LanguageTag.English;
        }

        private void Awake()
        {
            defaultActor = SubTag.DEFAULT;
            defaultLanguage = LanguageTag.English;
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().LoadingGameData
                += ActorData_LoadingGameData;
        }

        private XElement TryGetData(SubTag subTag, ActorDataTag actorData)
        {
            return xmlFile
                .Element(subTag.ToString())
                .Element(actorData.ToString());
        }

        private XElement TryGetData(SubTag subTag, ActorDataTag actorData,
            LanguageTag language)
        {
            return xmlFile
                .Element(subTag.ToString())
                .Element(actorData.ToString())
                .Element(language.ToString());
        }
    }
}
