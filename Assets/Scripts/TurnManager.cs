﻿using AxeMan.DungeonObject;
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

    public class EndingTurnEventArgs : EventArgs
    {
        public EndingTurnEventArgs(int objectID)
        {
            ObjectID = objectID;
        }

        public int ObjectID { get; }
    }

    public class StartingTurnEventArgs : EventArgs
    {
        public StartingTurnEventArgs(int objectID)
        {
            ObjectID = objectID;
        }

        public int ObjectID { get; }
    }

    public class TurnManager : MonoBehaviour, ITurnManager
    {
        public event EventHandler<EndingTurnEventArgs> EndingTurn;

        public event EventHandler<StartingTurnEventArgs> StartingTurn;

        public void EndTurn()
        {
            int id = GetComponent<Schedule>().Current
                .GetComponent<MetaInfo>().ObjectID;
            OnEndingTurn(new EndingTurnEventArgs(id));
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
            int id = GetComponent<Schedule>().Current
                .GetComponent<MetaInfo>().ObjectID;
            OnStartingTurn(new StartingTurnEventArgs(id));
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
            GetComponent<PublishAction>().TakenAction
                += TurnManager_TakenAction;
        }

        private void TurnManager_TakenAction(object sender,
            TakenActionEventArgs e)
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
