using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameEvent
{
    public class PublishAction : MonoBehaviour
    {
        public event EventHandler<TakenActionEventArgs> TakenAction;

        public event EventHandler<TakingActionEventArgs> TakingAction;

        public void ActorTakenAction(TakenActionEventArgs e)
        {
            OnTakenAction(e);
        }

        public void ActorTakingAction(TakingActionEventArgs e)
        {
            OnTakingAction(e);
        }

        protected virtual void OnTakenAction(TakenActionEventArgs e)
        {
            TakenAction?.Invoke(this, e);
        }

        protected virtual void OnTakingAction(TakingActionEventArgs e)
        {
            TakingAction?.Invoke(this, e);
        }
    }

    public class TakenActionEventArgs : EventArgs
    {
        public TakenActionEventArgs(GameObject actor, ActionTag action)
        {
            Actor = actor;
            Action = action;
        }

        public ActionTag Action { get; }

        public GameObject Actor { get; }
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
