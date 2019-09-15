using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameEvent
{
    public class ChangedHPEventArgs : EventArgs
    {
        public ChangedHPEventArgs(SubTag subTag, int id, int hp, bool isAlive)
        {
            SubTag = subTag;
            ID = id;
            CurrentHP = hp;
            IsAlive = isAlive;
        }

        public int CurrentHP { get; }

        public int ID { get; }

        public bool IsAlive { get; }

        public SubTag SubTag { get; }
    }

    public class PublishHP : MonoBehaviour
    {
        public event EventHandler<ChangedHPEventArgs> ChangedHP;

        public void PublishChangedHP(ChangedHPEventArgs e)
        {
            OnChangedHP(e);
        }

        protected virtual void OnChangedHP(ChangedHPEventArgs e)
        {
            ChangedHP?.Invoke(this, e);
        }
    }
}
