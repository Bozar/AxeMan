using AxeMan.Actor;
using UnityEngine;

namespace AxeMan.GameSystem.SchedulingSystem
{
    public interface ITurnManager
    {
        void EndTurn();

        GameObject NextTurn();

        void StartTurn();
    }

    public class TurnManager : MonoBehaviour, ITurnManager
    {
        public void EndTurn()
        {
            GameObject actor = GetComponent<Schedule>().Current;
            int[] pos = GetComponent<ConvertCoordinate>().Convert(
                actor.transform.position);
            Debug.Log("End: " + actor.GetComponent<MetaInfo>().STag + ", "
                + pos[0] + ", " + pos[1]);
        }

        public GameObject NextTurn()
        {
            EndTurn();
            GetComponent<Schedule>().GotoNext();
            StartTurn();

            return GetComponent<Schedule>().Current;
        }

        public void StartTurn()
        {
            GameObject actor = GetComponent<Schedule>().Current;
            int[] pos = GetComponent<ConvertCoordinate>().Convert(
               actor.transform.position);
            Debug.Log("Start: " + actor.GetComponent<MetaInfo>().STag + ", "
                + pos[0] + ", " + pos[1]);
        }
    }
}
