﻿using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SaveLoadGameFile;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface ISkillTemplateData
    {
        Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag);

        SkillTypeTag GetSkillTypeTag(SkillNameTag skillNameTag);
    }

    public class SkillTemplateData : MonoBehaviour, ISkillTemplateData
    {
        private string currentTemplate;
        private string directory;

        private Dictionary<SkillNameTag, Dictionary<SkillSlotTag,
            SkillComponentTag>> nameSlotCompDict;

        private Dictionary<SkillNameTag, SkillTypeTag> nameTypeDict;
        private SkillNameTag[] skillNames;
        private SkillSlotTag[] skillSlots;
        private XElement xmlFile;

        public Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag)
        {
            if (nameSlotCompDict.TryGetValue(skillNameTag, out var slotComp))
            {
                return new Dictionary<SkillSlotTag, SkillComponentTag>(slotComp);
            }
            return null;
        }

        public SkillTypeTag GetSkillTypeTag(SkillNameTag skillNameTag)
        {
            if (nameTypeDict.TryGetValue(skillNameTag, out SkillTypeTag data))
            {
                return data;
            }
            return SkillTypeTag.INVALID;
        }

        private void Awake()
        {
            currentTemplate = "skillTemplate.xml";
            directory = "Data";

            skillNames = new SkillNameTag[]
            {
                SkillNameTag.SkillQ,
                SkillNameTag.SkillW,
                SkillNameTag.SkillE,
                SkillNameTag.SkillR,
            };

            skillSlots = new SkillSlotTag[]
            {
                SkillSlotTag.Merit1,
                SkillSlotTag.Merit2,
                SkillSlotTag.Merit3,
                SkillSlotTag.Flaw1,
                SkillSlotTag.Flaw2,
                SkillSlotTag.Flaw3,
            };

            nameTypeDict = new Dictionary<SkillNameTag, SkillTypeTag>();

            nameSlotCompDict = new Dictionary<SkillNameTag,
                Dictionary<SkillSlotTag, SkillComponentTag>>();
            foreach (SkillNameTag snt in skillNames)
            {
                nameSlotCompDict[snt]
                    = new Dictionary<SkillSlotTag, SkillComponentTag>();
            }
        }

        private void InitializeSkillSlot()
        {
            foreach (SkillNameTag name in skillNames)
            {
                foreach (SkillSlotTag slot in skillSlots)
                {
                    if (TryGetData(name, slot, out XElement xElement)
                        && Enum.TryParse((string)xElement,
                        out SkillComponentTag data)
                        && (data != SkillComponentTag.INVALID))
                    {
                        nameSlotCompDict[name][slot] = data;
                    }
                }
            }
        }

        private void InitializeSkillType()
        {
            foreach (SkillNameTag snt in skillNames)
            {
                if (TryGetData(snt, SkillSlotTag.SkillType,
                    out XElement xElement))
                {
                    Enum.TryParse((string)xElement, out SkillTypeTag data);
                    nameTypeDict[snt] = data;
                }
            }
        }

        private void LoadFile()
        {
            xmlFile = GetComponent<SaveLoadXML>().Load(currentTemplate,
                directory);
        }

        private void SetTemplateData(SkillNameTag skillName,
            SkillSlotTag skillSlot, SkillComponentTag skillComponent)
        {
            nameSlotCompDict[skillName][skillSlot] = skillComponent;
            xmlFile
                .Element(skillName.ToString())
                .Element(skillSlot.ToString())
                .Value
                = skillComponent.ToString();
        }

        private void SetTemplateData(SkillNameTag skillName,
            SkillTypeTag skillType)
        {
            SkillSlotTag skillSlot = SkillSlotTag.SkillType;

            nameTypeDict[skillName] = skillType;
            xmlFile
                .Element(skillName.ToString())
                .Element(skillSlot.ToString())
                .Value
                = skillType.ToString();
        }

        private void SkillTemplateData_LoadingGameData(object sender,
            EventArgs e)
        {
            LoadFile();
            InitializeSkillType();
            InitializeSkillSlot();
        }

        private void SkillTemplateData_SwitchingGameMode(object sender,
            SwitchGameModeEventArgs e)
        {
            if ((e.LeaveMode == GameModeTag.BuildSkillMode)
               && (e.EnterMode == GameModeTag.StartMode))
            {
                GetComponent<SaveLoadXML>().Save(xmlFile, currentTemplate,
                    directory);
            }
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += SkillTemplateData_LoadingGameData;
            GetComponent<GameModeManager>().SwitchingGameMode
                += SkillTemplateData_SwitchingGameMode;
        }

        private bool TryGetData(SkillNameTag skillName, SkillSlotTag skillSlot,
            out XElement xElement)
        {
            xElement = xmlFile
                .Element(skillName.ToString())
                .Element(skillSlot.ToString());

            return xElement != null;
        }
    }
}
