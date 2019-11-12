using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface ILogData
    {
        string GetStringData(LogMessage logMessage);
    }

    public class LogData : MonoBehaviour, ILogData
    {
        public string GetStringData(LogMessage logMessage)
        {
            return "Hello World";
        }

        private void Start()
        {
        }
    }
}
