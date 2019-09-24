using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkillSlot
    {
        Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag);

        void RemoveSkillComponent(SkillNameTag skillNameTag,
            SkillSlotTag skillSlotTag);

        bool TrySetSkillSlot(SkillNameTag skillNameTag,
            SkillSlotTag skillSlotTag, SkillComponentTag skillComponentTag);
    }

    public class SkillSlot : MonoBehaviour, ISkillSlot
    {
        private SkillNameTag[] skillNameTags;

        private Dictionary<SkillNameTag,
            Dictionary<SkillSlotTag, SkillComponentTag>> slotCompDict;

        private Dictionary<SkillTypeTag, SkillComponentTag[]> validMeritSlot;

        public Dictionary<SkillSlotTag, SkillComponentTag> GetSkillSlot(
            SkillNameTag skillNameTag)
        {
            if (slotCompDict.TryGetValue(skillNameTag, out var slotComp))
            {
                return new Dictionary<SkillSlotTag, SkillComponentTag>(slotComp);
            }
            return null;
        }

        public void RemoveSkillComponent(SkillNameTag skillNameTag,
            SkillSlotTag skillSlotTag)
        {
            if (slotCompDict.TryGetValue(skillNameTag, out var slotComp))
            {
                slotComp.Remove(skillSlotTag);
            }
        }

        public bool TrySetSkillSlot(SkillNameTag skillNameTag,
            SkillSlotTag skillSlotTag, SkillComponentTag skillComponentTag)
        {
            SkillTypeTag skillTypeTag = GetComponent<PCSkillManager>()
                .GetSkillTypeTag(skillNameTag);
            bool canSetSlot;

            switch (skillSlotTag)
            {
                case SkillSlotTag.Merit1:
                case SkillSlotTag.Merit2:
                case SkillSlotTag.Merit3:
                    canSetSlot = VerifyMeritSlot(skillTypeTag, skillComponentTag);
                    break;

                case SkillSlotTag.Flaw1:
                case SkillSlotTag.Flaw2:
                case SkillSlotTag.Flaw3:
                    canSetSlot = VerifyFlawSlot(skillComponentTag);
                    break;

                default:
                    canSetSlot = false;
                    break;
            }

            if (canSetSlot
                && slotCompDict.TryGetValue(skillNameTag, out var slotComp))
            {
                slotComp[skillSlotTag] = skillComponentTag;
                return true;
            }
            return false;
        }

        private void Awake()
        {
            slotCompDict = new Dictionary<SkillNameTag,
                Dictionary<SkillSlotTag, SkillComponentTag>>();
            skillNameTags = new SkillNameTag[]
            {
                SkillNameTag.SkillQ,
                SkillNameTag.SkillW,
                SkillNameTag.SkillE,
                SkillNameTag.SkillR,
            };

            foreach (SkillNameTag snt in skillNameTags)
            {
                slotCompDict[snt]
                    = new Dictionary<SkillSlotTag, SkillComponentTag>();
            }

            validMeritSlot
                = new Dictionary<SkillTypeTag, SkillComponentTag[]>()
                {
                    { SkillTypeTag.Move,
                        new SkillComponentTag[]
                        {
                            SkillComponentTag.AirMerit,
                        }
                    },
                    { SkillTypeTag.Attack,
                        new SkillComponentTag[]
                        {
                            SkillComponentTag.AirMerit,
                            SkillComponentTag.AirCurse,
                        }
                    },
                    { SkillTypeTag.Buff,
                        new SkillComponentTag[]
                        {
                            SkillComponentTag.FireMerit,
                            SkillComponentTag.WaterMerit,
                            SkillComponentTag.AirMerit,
                            SkillComponentTag.EarthMerit,
                        }
                    },
                    { SkillTypeTag.Curse,
                        new SkillComponentTag[]
                        {
                            SkillComponentTag.AirMerit,

                            SkillComponentTag.FireCurse,
                            SkillComponentTag.WaterCurse,
                            SkillComponentTag.AirCurse,
                            SkillComponentTag.EarthCurse,
                        }
                    },
                };
        }

        private void LoadSkillSlot()
        {
            Dictionary<SkillSlotTag, SkillComponentTag> slotCompDict;

            foreach (SkillNameTag name in skillNameTags)
            {
                slotCompDict = GameCore.AxeManCore
                    .GetComponent<SkillTemplateData>().GetSkillSlot(name);
                foreach (SkillSlotTag slot in slotCompDict.Keys)
                {
                    TrySetSkillSlot(name, slot, slotCompDict[slot]);
                }
            }
        }

        private void SkillSlot_CreatedWorld(object sender, EventArgs e)
        {
            LoadSkillSlot();
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<InitializeMainGame>().CreatedWorld
                += SkillSlot_CreatedWorld;
        }

        private bool VerifyFlawSlot(SkillComponentTag skillComponentTag)
        {
            switch (skillComponentTag)
            {
                case SkillComponentTag.FireFlaw:
                case SkillComponentTag.WaterFlaw:
                case SkillComponentTag.AirFlaw:
                case SkillComponentTag.EarthFlaw:
                    return true;

                default:
                    return false;
            }
        }

        private bool VerifyMeritSlot(SkillTypeTag skillTypeTag,
            SkillComponentTag skillComponentTag)
        {
            if (validMeritSlot.TryGetValue(skillTypeTag, out var comp))
            {
                return Array.Exists(comp, e => e == skillComponentTag);
            }
            return false;
        }
    }
}
