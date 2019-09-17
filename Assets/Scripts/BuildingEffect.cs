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
        public SkillComponentTag GetEffect(MainTag mainTag, SubTag subTag)
        {
            string effect = (string)GetComponent<ActorData>().GetXElementData(
                mainTag, subTag, ActorDataTag.BuildingEffect);
            Enum.TryParse(effect, out SkillComponentTag skill);

            return skill;
        }

        public int GetPowerDuration(MainTag mainTag, SubTag subTag)
        {
            return GetComponent<ActorData>().GetIntData(mainTag, subTag,
                ActorDataTag.PowerDuration);
        }

        private void BuildingEffect_UpgradingAltar(object sender, EventArgs e)
        {
            Debug.Log("Building");
        }

        private void Start()
        {
            GetComponent<UpgradeAltar>().UpgradingAltar
                += BuildingEffect_UpgradingAltar;
        }
    }
}
