using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
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

    public class StartOrEndTurnEventArgs : EventArgs
    {
        public StartOrEndTurnEventArgs(SubTag subTag, int objectID)
        {
            SubTag = subTag;
            ObjectID = objectID;
        }

        public int ObjectID { get; }

        public SubTag SubTag { get; }
    }

    public class TurnManager : MonoBehaviour, ITurnManager
    {
        public event EventHandler<StartOrEndTurnEventArgs> EndedTurn;

        public event EventHandler<StartOrEndTurnEventArgs> EndingTurn;

        public event EventHandler<StartOrEndTurnEventArgs> StartedTurn;

        public event EventHandler<StartOrEndTurnEventArgs> StartingTurn;

        public void EndTurn()
        {
            var ea = GetEventArg();

            OnEndingTurn(ea);
            OnEndedTurn(ea);
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
            var ea = GetEventArg();

            OnStartingTurn(ea);
            OnStartedTurn(ea);
        }

        protected virtual void OnEndedTurn(StartOrEndTurnEventArgs e)
        {
            EndedTurn?.Invoke(this, e);
        }

        protected virtual void OnEndingTurn(StartOrEndTurnEventArgs e)
        {
            EndingTurn?.Invoke(this, e);
        }

        protected virtual void OnStartedTurn(StartOrEndTurnEventArgs e)
        {
            StartedTurn?.Invoke(this, e);
        }

        protected virtual void OnStartingTurn(StartOrEndTurnEventArgs e)
        {
            StartingTurn?.Invoke(this, e);
        }

        private StartOrEndTurnEventArgs GetEventArg()
        {
            SubTag subTag = GetComponent<Schedule>().Current
               .GetComponent<MetaInfo>().SubTag;
            int id = GetComponent<Schedule>().Current
                .GetComponent<MetaInfo>().ObjectID;

            return new StartOrEndTurnEventArgs(subTag, id);
        }

        private void Start()
        {
            GetComponent<PublishAction>().CheckingSchedule
                += TurnManager_CheckingSchedule;
        }

        private void TurnManager_CheckingSchedule(object sender,
            PublishActionEventArgs e)
        {
            if (!GetComponent<Schedule>().Current
                .GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            switch (e.Action)
            {
                case ActionTag.Skip:
                case ActionTag.Move:
                case ActionTag.UseSkillQ:
                case ActionTag.UseSkillW:
                case ActionTag.UseSkillE:
                case ActionTag.UseSkillR:
                    NextActor();
                    break;

                default:
                    break;
            }
        }
    }
}
