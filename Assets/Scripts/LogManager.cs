using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface ILogManager
    {
        void Add(LogMessage logMessage);

        void Add(string message);

        // The index of the last line is 0. The second last one is 1.
        string GetLog(int reverseIndex);
    }

    public class LogManager : MonoBehaviour, ILogManager
    {
        private List<string> fullLog;
        private int maxLogLength;
        private int minLogLength;

        public event EventHandler<EventArgs> AddingLog;

        public void Add(LogMessage logMessage)
        {
            string message = GetComponent<LogData>().GetStringData(logMessage);
            Add(message);
        }

        public void Add(string message)
        {
            fullLog.Add(message);
            ReduceLogLength();
            OnAddingLog(EventArgs.Empty);
        }

        public string GetLog(int reverseIndex)
        {
            int index = fullLog.Count - reverseIndex - 1;

            if ((index < 0) || (index > fullLog.Count - 1))
            {
                return "";
            }
            return fullLog[index];
        }

        protected virtual void OnAddingLog(EventArgs e)
        {
            AddingLog?.Invoke(this, e);
        }

        private void Awake()
        {
            fullLog = new List<string>();
            minLogLength = 20;
            maxLogLength = 200;
        }

        private void ReduceLogLength()
        {
            if (fullLog.Count > maxLogLength)
            {
                fullLog.RemoveRange(0, maxLogLength - minLogLength);
            }
        }
    }

    public class LogMessage
    {
        public LogMessage(LogCategoryTag category, LogMessageTag message)
        {
            LogCategoryTag = category;
            LogMessageTag = message;
        }

        public LogMessage(LogCategoryTag category, LogMessageTag message,
            SubTag actor) : this(category, message)
        {
            ActorTag = actor;
        }

        public LogMessage(LogCategoryTag category, LogMessageTag message,
            SubTag actor, SubTag trap) : this(category, message, actor)
        {
            TrapTag = trap;
        }

        public SubTag ActorTag { get; }

        public LogCategoryTag LogCategoryTag { get; }

        public LogMessageTag LogMessageTag { get; }

        public SubTag TrapTag { get; }
    }
}
