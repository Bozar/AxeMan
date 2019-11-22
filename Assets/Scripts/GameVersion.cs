using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IGameVersion
    {
        string Version { get; }
    }

    public class GameVersion : MonoBehaviour, IGameVersion
    {
        public string Version { get; private set; }

        private void Awake()
        {
            Version = "0.0.2";
        }
    }
}
