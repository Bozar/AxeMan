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
        public TakenActionEventArgs(ActionTag action,
            MainTag mainTag, SubTag subTag, int objectID)
        {
            Action = action;
            MainTag = mainTag;
            SubTag = subTag;
            ObjectID = objectID;
        }

        public ActionTag Action { get; }

        public MainTag MainTag { get; }

        public int ObjectID { get; }

        public SubTag SubTag { get; }
    }

    public class TakingActionEventArgs : EventArgs
    {
        public TakingActionEventArgs(ActionTag action,
            MainTag mainTag, SubTag subTag, int objectID)
        {
            Action = action;
            MainTag = mainTag;
            SubTag = subTag;
            ObjectID = objectID;
        }

        public ActionTag Action { get; }

        public MainTag MainTag { get; }

        public int ObjectID { get; }

        public SubTag SubTag { get; }
    }
}
