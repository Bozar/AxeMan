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
            if (!TryGetData(mainTag, subTag, actorData, out XElement data))
            {
                TryGetData(mainTag, defaultActor, actorData, out data);
            }
            return (int)data;
        }

        public string GetStringData(MainTag mainTag, SubTag subTag,
            ActorDataTag actorData)
        {
            if (!TryGetData(mainTag, subTag, actorData, userLanguage,
                out XElement data))
            {
                TryGetData(mainTag, subTag, actorData, defaultLanguage, out data);
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
            GetComponent<InitializeStartScreen>().LoadingGameData
                += ActorData_LoadingGameData;
        }

        private bool TryGetData(MainTag mainTag, SubTag subTag,
            ActorDataTag actorData, out XElement xElement)
        {
            if (mainTagXElement.TryGetValue(mainTag, out xElement))
            {
                xElement = xElement
                    .Element(subTag.ToString())
                    .Element(actorData.ToString());

                return xElement != null;
            }
            return false;
        }

        private bool TryGetData(MainTag mainTag, SubTag subTag,
            ActorDataTag actorData, LanguageTag language, out XElement xElement)
        {
            if (TryGetData(mainTag, subTag, actorData, out xElement))
            {
                xElement = xElement
                    .Element(language.ToString());

                return xElement != null;
            }
            return false;
        }
    }
}
