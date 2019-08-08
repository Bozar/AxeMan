using AxeMan.GameSystem.GameDataTag;
using System;
using UnityEngine;

namespace AxeMan.GameSystem.GameEvent
{
    public class PublishAction : MonoBehaviour
    {
        // Publish this event after TakenAction. We need to make sure that
        // everything is done before trying to end current actor's turn.
        public event EventHandler<PublishActionEventArgs> CheckingSchedule;

        public event EventHandler<PublishActionEventArgs> TakenAction;

        public event EventHandler<PublishActionEventArgs> TakingAction;

        public void ActorCheckingSchedule(PublishActionEventArgs e)
        {
            OnCheckingSchedule(e);
        }

        public void ActorTakenAction(PublishActionEventArgs e)
        {
            OnTakenAction(e);
        }

        public void ActorTakingAction(PublishActionEventArgs e)
        {
            OnTakingAction(e);
        }

        protected virtual void OnCheckingSchedule(PublishActionEventArgs e)
        {
            CheckingSchedule?.Invoke(this, e);
        }

        protected virtual void OnTakenAction(PublishActionEventArgs e)
        {
            TakenAction?.Invoke(this, e);
        }

        protected virtual void OnTakingAction(PublishActionEventArgs e)
        {
            TakingAction?.Invoke(this, e);
        }
    }

    public class PublishActionEventArgs : EventArgs
    {
        public PublishActionEventArgs(ActionTag action,
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
