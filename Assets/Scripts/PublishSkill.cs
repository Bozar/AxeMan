using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameEvent
{
    public class ChangedSkillCooldownEventArgs : EventArgs
    {
        public ChangedSkillCooldownEventArgs(SkillNameTag skillNameTag)
        {
            SkillNameTag = skillNameTag;
        }

        public SkillNameTag SkillNameTag { get; }
    }

    public class PublishSkill : MonoBehaviour
    {
        public event EventHandler<ChangedSkillCooldownEventArgs>
            ChangedSkillCooldown;

        public void PublishSkillCooldown(ChangedSkillCooldownEventArgs e)
        {
            OnChangedSkillCooldown(e);
        }

        protected virtual void OnChangedSkillCooldown(
            ChangedSkillCooldownEventArgs e)
        {
            ChangedSkillCooldown?.Invoke(this, e);
        }
    }
}
