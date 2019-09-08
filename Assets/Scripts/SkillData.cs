﻿using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SaveLoadGameFile;
using System;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface ISkillData
    {
        string GetLongSkillTypeName(SkillTypeTag skillTypeTag);

        string GetShortSkillTypeName(SkillTypeTag skillTypeTag);

        string GetSkillComponentName(SkillComponentTag skillComponentTag);

        string GetSkillName(SkillNameTag skillNameTag);
    }

    public class SkillData : MonoBehaviour, ISkillData
    {
        private string component;
        private LanguageTag defaultLanguage;
        private string error;
        private string longType;
        private string shortType;
        private string skillName;
        private LanguageTag userLanguage;
        private XElement xmlFile;

        public string GetLongSkillTypeName(SkillTypeTag skillTypeTag)
        {
            if (TryGetData(longType, skillTypeTag.ToString(),
                userLanguage.ToString(), out XElement data)
                || TryGetData(longType, skillTypeTag.ToString(),
                defaultLanguage.ToString(), out data))
            {
                return (string)data;
            }
            return error;
        }

        public string GetShortSkillTypeName(SkillTypeTag skillTypeTag)
        {
            if (TryGetData(shortType, skillTypeTag.ToString(),
                userLanguage.ToString(), out XElement data)
                || TryGetData(shortType, skillTypeTag.ToString(),
                defaultLanguage.ToString(), out data))
            {
                return (string)data;
            }
            return error;
        }

        public string GetSkillComponentName(SkillComponentTag skillComponentTag)
        {
            if (TryGetData(component, skillComponentTag.ToString(),
               userLanguage.ToString(), out XElement data)
               || TryGetData(component, skillComponentTag.ToString(),
               defaultLanguage.ToString(), out data))
            {
                return (string)data;
            }
            return error;
        }

        public string GetSkillName(SkillNameTag skillNameTag)
        {
            if (TryGetData(skillName, skillNameTag.ToString(),
              userLanguage.ToString(), out XElement data)
              || TryGetData(skillName, skillNameTag.ToString(),
              defaultLanguage.ToString(), out data))
            {
                return (string)data;
            }
            return error;
        }

        private void Awake()
        {
            error = "INVALID_NAME";
            skillName = "Name";
            component = "Component";
            shortType = "ShortType";
            longType = "LongType";

            defaultLanguage = LanguageTag.English;
        }

        private void LoadSkillData()
        {
            string file = "skillData.xml";
            string directory = "Data";

            xmlFile = GetComponent<SaveLoadXML>().Load(file, directory);
        }

        private void LoadUserLanguage()
        {
            string language = GetComponent<SettingData>().GetStringData(
               SettingDataTag.Language);
            Enum.TryParse(language, out userLanguage);
        }

        private void SkillData_LoadingGameData(object sender, EventArgs e)
        {
            LoadUserLanguage();
            LoadSkillData();
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += SkillData_LoadingGameData;
        }

        private bool TryGetData(string dataType, string tag, string language,
            out XElement data)
        {
            data = xmlFile
                .Element(dataType)
                .Element(tag)
                .Element(language);

            return data != null;
        }
    }
}