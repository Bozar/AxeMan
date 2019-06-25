using AxeMan.Actor;
using AxeMan.GameSystem.ObjectFactory;
using System.Collections.Generic;
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
        private List<GameObject> schedule;

        public GameObject Current { get { return null; } }

        public void Add(GameObject actor)
        {
            schedule.Add(actor);
        }

        public GameObject GotoNext()
        {
            throw new System.NotImplementedException();
        }

        public void Print()
        {
            int[] position;

            foreach (GameObject s in schedule)
            {
                position = GetComponent<ConvertCoordinate>().Convert(
                    s.transform.position);
                Debug.Log(s.GetComponent<MetaInfo>().STag + ": "
                    + position[0] + ", " + position[1]);
            }
        }

        public void Remove(GameObject actor)
        {
            schedule.Remove(actor);
        }

        private void Awake()
        {
            schedule = new List<GameObject>();
        }

        private void Schedule_CreatedObject(object sender,
            CreatedObjectEventArgs e)
        {
            if (e.Data.GetComponent<MetaInfo>().MTag != MainTag.Actor)
            {
                return;
            }
            Add(e.Data);
        }

        private void Start()
        {
            GetComponent<CreateObject>().CreatedObject
                += Schedule_CreatedObject;
        }
    }
}
