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

        void Curse();

        void DealDamage();

        bool IsInsideRage(out int outOfRange);
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
                return Math.Max(0, baseDamage + ModDamage());
            }
        }

        public void Curse()
        {
            if (CurseEffect != SkillComponentTag.INVALID)
            {
                pc.GetComponent<ActorStatus>().AddStatus(CurseEffect,
                    new EffectData(CurseData, CurseData));
            }
        }

        public void DealDamage()
        {
            pc.GetComponent<HP>().Subtract(Damage);
        }

        public bool IsInsideRage(out int outOfRange)
        {
            int[] npcPos = GetComponent<MetaInfo>().Position;
            int[] pcPos = pc.GetComponent<MetaInfo>().Position;
            int range = GameCore.AxeManCore.GetComponent<Distance>()
                .GetDistance(npcPos, pcPos);

            outOfRange = range - AttackRange;
            return AttackRange >= range;
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

        private int ModDamage()
        {
            int damage = 0;

            if (pc.GetComponent<ActorStatus>().HasStatus(
                SkillComponentTag.AirFlaw, out EffectData airFlaw))
            {
                damage += airFlaw.Power;
            }
            if (pc.GetComponent<ActorStatus>().HasStatus(
                SkillComponentTag.EarthMerit, out EffectData earthMerit))
            {
                damage -= earthMerit.Power;
            }
            return damage;
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
