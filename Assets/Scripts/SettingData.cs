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

        private void Awake()
        {
            error = "Setting not found.";
        }

        private void SettingData_LoadingGameData(object sender, EventArgs e)
        {
            string file = "setting.xml";
            string directory = "Data";

            xmlFile = GetComponent<SaveLoadXML>().Load(file, directory);
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += SettingData_LoadingGameData;
        }

        private bool TryGetData(SettingDataTag settingData, out XElement data)
        {
            data = xmlFile.Element(settingData.ToString());
            return data != null;
        }
    }
}
