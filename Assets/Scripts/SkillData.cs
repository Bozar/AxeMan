using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SaveLoadGameFile;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface ISkillData
    {
        bool ConvertCurse2Flaw(SkillComponentTag curse,
            out SkillComponentTag flaw);

        string GetLongSkillComponentName(SkillComponentTag skillComponentTag);

        string GetLongSkillTypeName(SkillTypeTag skillTypeTag);

        string GetShortSkillTypeName(SkillTypeTag skillTypeTag);

        string GetSkillComponentDescription(SkillComponentTag skillComponentTag);

        string GetSkillComponentName(SkillComponentTag skillComponentTag);

        string GetSkillName(SkillNameTag skillNameTag);
    }

    public class SkillData : MonoBehaviour, ISkillData
    {
        private string component;
        private Dictionary<SkillComponentTag, SkillComponentTag> curse2flaw;
        private string defaultLanguage;
        private string description;
        private string error;
        private string longComponent;
        private string longType;
        private string shortType;
        private string skillName;
        private string userLanguage;
        private XElement xmlFile;

        public bool ConvertCurse2Flaw(SkillComponentTag curse,
            out SkillComponentTag flaw)
        {
            if (curse2flaw.TryGetValue(curse, out SkillComponentTag data))
            {
                flaw = data;
                return true;
            }
            flaw = SkillComponentTag.INVALID;
            return false;
        }

        public string GetLongSkillComponentName(
            SkillComponentTag skillComponentTag)
        {
            return GetStringData(longComponent, skillComponentTag.ToString());
        }

        public string GetLongSkillTypeName(SkillTypeTag skillTypeTag)
        {
            return GetStringData(longType, skillTypeTag.ToString());
        }

        public string GetShortSkillTypeName(SkillTypeTag skillTypeTag)
        {
            return GetStringData(shortType, skillTypeTag.ToString());
        }

        public string GetSkillComponentDescription(
            SkillComponentTag skillComponentTag)
        {
            return GetStringData(description, skillComponentTag.ToString());
        }

        public string GetSkillComponentName(SkillComponentTag skillComponentTag)
        {
            return GetStringData(component, skillComponentTag.ToString());
        }

        public string GetSkillName(SkillNameTag skillNameTag)
        {
            return GetStringData(skillName, skillNameTag.ToString());
        }

        private void Awake()
        {
            error = "INVALID_NAME";
            skillName = "Name";
            description = "Description";
            component = "Component";
            longComponent = "LongComponent";
            shortType = "ShortType";
            longType = "LongType";

            defaultLanguage = LanguageTag.English.ToString();

            curse2flaw = new Dictionary<SkillComponentTag, SkillComponentTag>()
            {
                { SkillComponentTag.FireCurse, SkillComponentTag.FireFlaw },
                { SkillComponentTag.WaterCurse, SkillComponentTag.WaterFlaw },
                { SkillComponentTag.AirCurse, SkillComponentTag.AirFlaw },
                { SkillComponentTag.EarthCurse, SkillComponentTag.EarthFlaw },
            };
        }

        private string GetStringData(string dataType, string dataTag)
        {
            if (TryGetData(dataType, dataTag, userLanguage, out XElement data)
                || TryGetData(dataType, dataTag, defaultLanguage, out data))
            {
                return (string)data;
            }
            return error;
        }

        private void LoadSkillData()
        {
            string file = "skillData.xml";
            string directory = "Data";

            xmlFile = GetComponent<SaveLoadXML>().Load(file, directory);
        }

        private void LoadUserLanguage()
        {
            userLanguage = GetComponent<SettingData>().GetStringData(
                SettingDataTag.Language);
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
