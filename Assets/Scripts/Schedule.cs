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
        private int pointer;
        private List<GameObject> schedule;

        public GameObject Current
        {
            get
            {
                return (schedule.Count == 0)
                    ? null
                    : schedule[pointer];
            }
        }

        public void Add(GameObject actor)
        {
            schedule.Add(actor);
        }

        public GameObject GotoNext()
        {
            pointer++;
            pointer = ResetPointer(pointer, schedule.Count);
            return Current;
        }

        public void Print()
        {
            int[] position;
            string marker;

            foreach (GameObject s in schedule)
            {
                marker = s == Current ? "* " : "";
                position = GetComponent<ConvertCoordinate>().Convert(
                    s.transform.position);
                Debug.Log(marker + s.GetComponent<MetaInfo>().STag + ": "
                    + position[0] + ", " + position[1]);
            }
        }

        public void Remove(GameObject actor)
        {
            GameObject pointedActor = Current;
            if (actor == pointedActor)
            {
                pointedActor = GotoNext();
            }

            schedule.Remove(actor);
            pointer = schedule.IndexOf(pointedActor);
        }

        private void Awake()
        {
            schedule = new List<GameObject>();
            pointer = 0;
        }

        private int ResetPointer(int current, int length)
        {
            if ((current > length - 1) || (current < 0))
            {
                current = 0;
            }
            return current;
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

        private void Schedule_RemovingObject(object sender,
            RemovingObjectEventArgs e)
        {
            Remove(e.Data);
        }

        private void Start()
        {
            GetComponent<CreateObject>().CreatedObject
                += Schedule_CreatedObject;
            GetComponent<RemoveObject>().RemovingObject
                += Schedule_RemovingObject;
        }
    }
}
