using UnityEngine;

namespace AxeMan.GameSystem.SchedulingSystem
{
    public interface ISchedule
    {
        GameObject Current { get; }

        void Add(GameObject actor);

        GameObject GotoNext();

        void Remove(GameObject actor);
    }

    public class Schedule : MonoBehaviour, ISchedule
    {
        public GameObject Current { get { return null; } }

        public void Add(GameObject actor)
        {
            throw new System.NotImplementedException();
        }

        public GameObject GotoNext()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(GameObject actor)
        {
            throw new System.NotImplementedException();
        }
    }
}
