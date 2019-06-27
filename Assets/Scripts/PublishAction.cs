using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameEvent
{
    public class PublishAction : MonoBehaviour
    {
        public event EventHandler<TakingActionEventArgs> TakingAction;

        public void ActorTakingAction(TakingActionEventArgs e)
        {
            OnTakingAction(e);
        }

        protected virtual void OnTakingAction(TakingActionEventArgs e)
        {
            TakingAction?.Invoke(this, e);
        }
    }

    public class TakingActionEventArgs : EventArgs
    {
        public TakingActionEventArgs(GameObject actor, ActionTag action)
        {
            Actor = actor;
            Action = action;
        }

        public ActionTag Action { get; }

        public GameObject Actor { get; }
    }
}
