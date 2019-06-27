using System;
using UnityEngine;

namespace AxeMan.GameSystem.SchedulingSystem
{
    public interface ITurnManager
    {
        void EndTurn();

        GameObject NextActor();

        void StartTurn();
    }

    public class EndingTurnEventArgs : EventArgs
    {
        public EndingTurnEventArgs(GameObject data)
        {
            Data = data;
        }

        public GameObject Data { get; }
    }

    public class StartingTurnEventArgs : EventArgs
    {
        public StartingTurnEventArgs(GameObject data)
        {
            Data = data;
        }

        public GameObject Data { get; }
    }

    public class TurnManager : MonoBehaviour, ITurnManager
    {
        public event EventHandler<EndingTurnEventArgs> EndingTurn;

        public event EventHandler<StartingTurnEventArgs> StartingTurn;

        public void EndTurn()
        {
            GameObject actor = GetComponent<Schedule>().Current;
            OnEndingTurn(new EndingTurnEventArgs(actor));
        }

        public GameObject NextActor()
        {
            EndTurn();
            GetComponent<Schedule>().GotoNext();
            StartTurn();

            return GetComponent<Schedule>().Current;
        }

        public void StartTurn()
        {
            GameObject actor = GetComponent<Schedule>().Current;
            OnStartingTurn(new StartingTurnEventArgs(actor));
        }

        protected virtual void OnEndingTurn(EndingTurnEventArgs e)
        {
            EndingTurn?.Invoke(this, e);
        }

        protected virtual void OnStartingTurn(StartingTurnEventArgs e)
        {
            StartingTurn?.Invoke(this, e);
        }

        private void Start()
        {
            GetComponent<PublishAction>().TakingAction
                += TurnManager_TakingAction;
        }

        private void TurnManager_TakingAction(object sender,
            TakingActionEventArgs e)
        {
            if (e.Action == ActionTag.Skip)
            {
                NextActor();
            }
        }
    }
}
