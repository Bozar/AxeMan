using AxeMan.GameSystem.GameDataTag;
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
        private Dictionary<SkillNameTag,
            Dictionary<SkillSlotTag, SkillComponentTag>> nameSlotCompDict;

        private Dictionary<SkillNameTag, SkillTypeTag> nameTypeDict;
        private SkillNameTag[] skillNames;
        private SkillSlotTag[] skillSlots;
        private XElement templateFile;

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

        private void LoadFile()
        {
            string file = "skillTemplate.xml";
            string directory = "Data";

            templateFile = GetComponent<SaveLoadXML>().Load(file, directory);
        }

        private void SetSkillSlot()
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

        private void SetSkillType()
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

        private void SkillTemplateData_LoadingGameData(object sender,
            EventArgs e)
        {
            LoadFile();
            SetSkillType();
            SetSkillSlot();
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += SkillTemplateData_LoadingGameData;
        }

        private bool TryGetData(SkillNameTag skillName, SkillSlotTag skillSlot,
            out XElement xElement)
        {
            xElement = templateFile
                .Element(skillName.ToString())
                .Element(skillSlot.ToString());

            return xElement != null;
        }
    }
}
