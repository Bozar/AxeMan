using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.DungeonObject.ActorSkill
{
    public interface ISkill
    {
        int Cooldown { get; }

        int Damage { get; }

        int Range { get; }

        SkillEffect[] SkillEffects { get; }

        SkillNameTag SkillName { get; }

        SkillTypeTag SkillType { get; }
    }

    public class SkillEffect
    {
        public SkillEffect(SkillComponentTag skillComponent, int powerValue,
            int duration)
        {
            SkillComponent = skillComponent;
            PowerValue = powerValue;
            Duration = duration;
        }

        public int Duration { get; }

        public int PowerValue { get; }

        public SkillComponentTag SkillComponent { get; }
    }

    public class SkillPrototype : MonoBehaviour, ISkill
    {
        public int Cooldown { get; }

        public int Damage { get; }

        public int Range { get; }

        public SkillEffect[] SkillEffects { get; }

        public SkillNameTag SkillName { get; }

        public SkillTypeTag SkillType { get; }
    }
}
