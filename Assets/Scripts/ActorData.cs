using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SaveLoadGameFile;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface IActorData
    {
        int GetIntData(MainTag mainTag, SubTag subTag, ActorDataTag actorData);

        string GetStringData(MainTag mainTag, SubTag subTag,
            ActorDataTag actorData);
    }

    public class ActorData : MonoBehaviour, IActorData
    {
        private SubTag defaultActor;
        private LanguageTag defaultLanguage;
        private Dictionary<MainTag, XElement> mainTagXElement;
        private LanguageTag userLanguage;

        public int GetIntData(MainTag mainTag, SubTag subTag,
            ActorDataTag actorData)
        {
            XElement data = TryGetData(mainTag, subTag, actorData);

            if (data == null)
            {
                return (int)TryGetData(mainTag, defaultActor, actorData);
            }
            return (int)data;
        }

        public string GetStringData(MainTag mainTag, SubTag subTag,
            ActorDataTag actorData)
        {
            XElement data = TryGetData(mainTag, subTag, actorData, userLanguage);

            if (data == null)
            {
                return (string)TryGetData(mainTag, subTag, actorData,
                    defaultLanguage);
            }
            return (string)data;
        }

        private void ActorData_LoadingGameData(object sender, EventArgs e)
        {
            LoadActorData();
            LoadUserLanguage();
        }

        private void Awake()
        {
            defaultActor = SubTag.DEFAULT;
            defaultLanguage = LanguageTag.English;

            mainTagXElement = new Dictionary<MainTag, XElement>();
        }

        private void LoadActorData()
        {
            string character = "charactorData.xml";
            string building = "buildingData.xml";
            string trap = "trapData.xml";
            string directory = "Data";

            MainTag[] mainTags = new MainTag[]
            {
                MainTag.Actor,
                MainTag.Building,
                MainTag.Floor,
                MainTag.Trap
            };
            string[] files = new string[]
            {
                character,
                building,
                building,
                trap
            };

            for (int i = 0; i < mainTags.Length; i++)
            {
                mainTagXElement[mainTags[i]]
                    = GetComponent<SaveLoadXML>().Load(files[i], directory);
            }
        }

        private void LoadUserLanguage()
        {
            // TODO: Retrieve language setting from another file.
            userLanguage = LanguageTag.English;
        }

        private void Start()
        {
            GetComponent<InitializeMainGame>().LoadingGameData
                += ActorData_LoadingGameData;
        }

        private XElement TryGetData(MainTag mainTag, SubTag subTag,
            ActorDataTag actorData)
        {
            if (mainTagXElement.TryGetValue(mainTag, out XElement xElement))
            {
                return xElement
                    .Element(subTag.ToString())
                    .Element(actorData.ToString());
            }
            return null;
        }

        private XElement TryGetData(MainTag mainTag, SubTag subTag,
            ActorDataTag actorData, LanguageTag language)
        {
            XElement xElement = TryGetData(mainTag, subTag, actorData);

            if (xElement != null)
            {
                return xElement.Element(language.ToString());
            }
            return null;
        }
    }
}
