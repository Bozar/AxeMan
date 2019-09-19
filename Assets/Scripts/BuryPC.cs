using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using System;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class BuryPC : MonoBehaviour
    {
        public event EventHandler<EventArgs> BuryingPC;

        protected virtual void OnBuryingPC(EventArgs e)
        {
            BuryingPC?.Invoke(this, e);
        }

        private void BuryPC_ChangedHP(object sender, ChangedHPEventArgs e)
        {
            if (e.IsAlive || (e.SubTag != SubTag.PC))
            {
                return;
            }
            OnBuryingPC(EventArgs.Empty);
        }

        private void Start()
        {
            GetComponent<PublishHP>().ChangedHP += BuryPC_ChangedHP;
        }
    }
}
