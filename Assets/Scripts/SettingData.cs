using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SaveLoadGameFile;
using System;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface ISettingData
    {
        bool GetBoolData(SettingDataTag settingData);

        string GetStringData(SettingDataTag settingData);
    }

    public class SettingData : MonoBehaviour, ISettingData
    {
        private string error;
        private XElement xmlFile;

        public bool GetBoolData(SettingDataTag settingData)
        {
            if (TryGetData(settingData, out XElement data))
            {
                return (bool)data;
            }
            throw new Exception(error);
        }

        public string GetStringData(SettingDataTag settingData)
        {
            if (TryGetData(settingData, out XElement data))
            {
                return (string)data;
            }
            throw new Exception(error);
        }

        private void Awake()
        {
            error = "Setting not found.";
        }

        private void SettingData_LoadingSettingData(object sender, EventArgs e)
        {
            string file = "setting.xml";
            string directory = "Data";

            xmlFile = GetComponent<SaveLoadXML>().Load(file, directory);
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingSettingData
                += SettingData_LoadingSettingData;
        }

        private bool TryGetData(SettingDataTag settingData, out XElement data)
        {
            data = xmlFile.Element(settingData.ToString());
            return data != null;
        }
    }
}
