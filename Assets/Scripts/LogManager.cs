using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface ILogManager
    {
        void Add(LogMessage logMessage);

        string[] GetLog(int logLength);
    }

    public class LogManager : MonoBehaviour, ILogManager
    {
        private List<string> fullLog;

        public void Add(LogMessage logMessage)
        {
            string message = GetComponent<LogData>().GetStringData(logMessage);
            fullLog.Add(message);
            // Remove overflowed message.
            PrintLog();
        }

        public string[] GetLog(int logLength)
        {
            throw new System.NotImplementedException();
        }

        private void Awake()
        {
            fullLog = new List<string>();
        }

        private void PrintLog()
        {
            foreach (string message in fullLog)
            {
                Debug.Log(message);
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
