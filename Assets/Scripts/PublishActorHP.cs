using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameEvent
{
    public class ChangeHPEventArgs : EventArgs
    {
        public ChangeHPEventArgs(SubTag subTag, int id, int hp, bool isAlive)
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

    public class PublishActorHP : MonoBehaviour
    {
        public event EventHandler<ChangeHPEventArgs> ChangedHP;

        public event EventHandler<ChangeHPEventArgs> ChangingHP;

        public void PublishHP(ChangeHPEventArgs e)
        {
            OnChangingHP(e);
            OnChangedHP(e);
        }

        protected virtual void OnChangedHP(ChangeHPEventArgs e)
        {
            ChangedHP?.Invoke(this, e);
        }

        protected virtual void OnChangingHP(ChangeHPEventArgs e)
        {
            ChangingHP?.Invoke(this, e);
        }
    }
}
