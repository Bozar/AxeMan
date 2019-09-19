using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using System;
using System.Linq;
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
        private ActionTag[] endingTurnActions;
        private bool pauseTurn;

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
            if (!pauseTurn)
            {
                StartTurn();
            }

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

        private void Awake()
        {
            pauseTurn = false;

            endingTurnActions = new ActionTag[]
            {
                ActionTag.Skip,
                ActionTag.Move,
                ActionTag.UseSkillQ,
                ActionTag.UseSkillW,
                ActionTag.UseSkillE,
                ActionTag.UseSkillR,
                ActionTag.ActiveAltar,
            };
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
            GetComponent<BuryPC>().BuryingPC += TurnManager_BuryingPC;
        }

        private void TurnManager_BuryingPC(object sender, EventArgs e)
        {
            pauseTurn = true;
        }

        private void TurnManager_CheckingSchedule(object sender,
            PublishActionEventArgs e)
        {
            if (!GetComponent<Schedule>().Current
                .GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            if (endingTurnActions.Contains(e.Action))
            {
                NextActor();
            }
        }
    }
}
