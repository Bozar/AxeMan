using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface INPCAttack
    {
        int AttackRange { get; }

        int CurseData { get; }

        SkillComponentTag CurseEffect { get; }

        int Damage { get; }

        bool IsInsideRage { get; }
    }

    public class NPCAttack : MonoBehaviour, INPCAttack
    {
        private int baseDamage;
        private int baseRange;
        private int minRange;
        private GameObject pc;

        public int AttackRange
        {
            get
            {
                if (GetComponent<ActorStatus>().HasStatus(
                    SkillComponentTag.EarthFlaw, out EffectData effect))
                {
                    return Math.Max(minRange, baseRange - effect.Power);
                }
                return baseRange;
            }
        }

        public int CurseData { get; private set; }

        public SkillComponentTag CurseEffect { get; private set; }

        public int Damage
        {
            get
            {
                // TODO: Change data based on actor status.
                return baseDamage;
            }
        }

        public bool IsInsideRage
        {
            get
            {
                int[] npcPos = GetComponent<MetaInfo>().Position;
                int[] pcPos = pc.GetComponent<MetaInfo>().Position;
                int range = GameCore.AxeManCore.GetComponent<Distance>()
                    .GetDistance(npcPos, pcPos);

                return AttackRange >= range;
            }
        }

        private void Awake()
        {
            MainTag mainTag = GetComponent<MetaInfo>().MainTag;
            SubTag subTag = GetComponent<MetaInfo>().SubTag;
            ActorDataTag range = ActorDataTag.AttackRange;
            ActorDataTag damage = ActorDataTag.Damage;
            ActorDataTag curseEffect = ActorDataTag.CurseEffect;
            ActorDataTag curseData = ActorDataTag.CurseData;

            baseRange = GameCore.AxeManCore.GetComponent<ActorData>()
                .GetIntData(mainTag, subTag, range);
            baseDamage = GameCore.AxeManCore.GetComponent<ActorData>()
               .GetIntData(mainTag, subTag, damage);

            Enum.TryParse((string)GameCore.AxeManCore.GetComponent<ActorData>()
                .GetXElementData(mainTag, subTag, curseEffect),
                out SkillComponentTag effect);
            CurseEffect = effect;
            CurseData = GameCore.AxeManCore.GetComponent<ActorData>()
              .GetIntData(mainTag, subTag, curseData);

            minRange = 1;
        }

        private void NPCAttack_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            pc = e.PC;
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<InitializeMainGame>()
                .SettingReference += NPCAttack_SettingReference;
        }
    }
}
