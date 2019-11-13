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
        private List<string> log;

        public void Add(LogMessage logMessage)
        {
            string message = GetComponent<LogData>().GetStringData(logMessage);
            log.Add(message);
            // Remove overflowed message.
        }

        public string[] GetLog(int logLength)
        {
            throw new System.NotImplementedException();
        }

        private void Awake()
        {
            log = new List<string>();
        }
    }

    public class LogMessage
    {
        public LogMessage(LogCategoryTag category, LogMessageTag message)
        {
            LogCategoryTag = category;
            LogMessageTag = message;
        }

        public SubTag ActorTag { get; }

        public LogCategoryTag LogCategoryTag { get; }

        public LogMessageTag LogMessageTag { get; }

        public SubTag TrapTag { get; }
    }
}
