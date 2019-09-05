using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IAltarEffect
    {
        SkillComponentTag Effect { get; }

        int PowerDuration { get; }
    }

    public class AltarEffect : MonoBehaviour, IAltarEffect
    {
        public SkillComponentTag Effect { get; private set; }

        public int PowerDuration { get; private set; }

        private void AltarEffect_CreatedWorld(object sender, EventArgs e)
        {
            Effect = SkillComponentTag.FireMerit;
            PowerDuration = 4;
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<InitializeMainGame>().CreatedWorld
                += AltarEffect_CreatedWorld;
        }
    }
}
