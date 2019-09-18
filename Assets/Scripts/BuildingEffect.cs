using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IBuildingEffect
    {
        SkillComponentTag GetEffect(MainTag mainTag, SubTag subTag);

        int GetPowerDuration(MainTag mainTag, SubTag subTag);
    }

    public class BuildingEffect : MonoBehaviour, IBuildingEffect
    {
        private int countUpgrade;

        public SkillComponentTag GetEffect(MainTag mainTag, SubTag subTag)
        {
            string effect = (string)GetComponent<ActorData>().GetXElementData(
                mainTag, subTag, ActorDataTag.BuildingEffect);
            Enum.TryParse(effect, out SkillComponentTag skill);

            return skill;
        }

        public int GetPowerDuration(MainTag mainTag, SubTag subTag)
        {
            int data;
            int baseAltar;
            int bonusAltar;

            if (mainTag == MainTag.Altar)
            {
                baseAltar = GetComponent<ActorData>().GetIntData(
                    mainTag, subTag, ActorDataTag.PowerDuration);
                bonusAltar = GetComponent<ActorData>().GetIntData(
                    mainTag, subTag, ActorDataTag.AddPowerDuration);

                data = baseAltar + countUpgrade * bonusAltar;
            }
            else
            {
                data = GetComponent<ActorData>().GetIntData(mainTag, subTag,
                    ActorDataTag.PowerDuration);
            }
            return data;
        }

        private void Awake()
        {
            countUpgrade = 0;
        }

        private void BuildingEffect_UpgradingAltar(object sender, EventArgs e)
        {
            countUpgrade++;
        }

        private void Start()
        {
            GetComponent<UpgradeAltar>().UpgradingAltar
                += BuildingEffect_UpgradingAltar;
        }
    }
}
