using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameEvent
{
    public class PublishActorStatus : MonoBehaviour
    {
        public event EventHandler<EventArgs> ChangedActorStatus;

        public void PublishChangedActorStatus()
        {
            OnChangedActorStatus(EventArgs.Empty);
        }

        protected virtual void OnChangedActorStatus(EventArgs e)
        {
            ChangedActorStatus?.Invoke(this, e);
        }
    }
}
