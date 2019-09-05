using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IAltarEffect
    {
        SkillComponentTag GetEffect(SubTag subTag);

        int GetPowerDuration(SubTag subTag);
    }

    public class AltarEffect : MonoBehaviour, IAltarEffect
    {
        public SkillComponentTag GetEffect(SubTag subTag)
        {
            string effect = (string)GetComponent<ActorData>().GetXElementData(
                MainTag.Building, subTag, ActorDataTag.AltarEffect);
            Enum.TryParse(effect, out SkillComponentTag skill);

            return skill;
        }

        public int GetPowerDuration(SubTag subTag)
        {
            return GetComponent<ActorData>().GetIntData(MainTag.Building, subTag,
                ActorDataTag.PowerDuration);
        }
    }
}
