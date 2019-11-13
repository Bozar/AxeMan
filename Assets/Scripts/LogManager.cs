using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface ILogManager
    {
        int LogLength { get; }

        void Add(LogMessage logMessage);

        string[] GetLog(int logLength);
    }

    public class LogManager : MonoBehaviour, ILogManager
    {
        private List<string> fullLog;
        private int maxLogLength;
        private int minLogLength;

        public int LogLength { get { return fullLog.Count; } }

        public void Add(LogMessage logMessage)
        {
            string message = GetComponent<LogData>().GetStringData(logMessage);
            fullLog.Add(message);

            ReduceLogLength();

            PrintLog();
        }

        public string[] GetLog(int logLength)
        {
            throw new System.NotImplementedException();
        }

        private void Awake()
        {
            fullLog = new List<string>();
            minLogLength = 20;
            maxLogLength = 200;
        }

        private void PrintLog()
        {
            foreach (string message in fullLog)
            {
                Debug.Log(message);
            }
        }

        private void ReduceLogLength()
        {
            if (LogLength > maxLogLength)
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
