using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameEvent
{
    public class ChangedHPEventArgs : EventArgs
    {
        public ChangedHPEventArgs(SubTag sTag, int currentHP)
        {
            STag = sTag;
            CurrentHP = currentHP;
        }

        public int CurrentHP { get; }

        public SubTag STag { get; }
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
