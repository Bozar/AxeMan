using AxeMan.Actor;
using UnityEngine;

namespace AxeMan.GameSystem.SchedulingSystem
{
    public interface ITurnManager
    {
        void EndTurn(GameObject actor);

        GameObject NextTurn();

        void StartTurn(GameObject actor);
    }

    public class TurnManager : MonoBehaviour, ITurnManager
    {
        public void EndTurn(GameObject actor)
        {
            int[] pos = GetComponent<ConvertCoordinate>().Convert(
                actor.transform.position);
            Debug.Log("End: " + actor.GetComponent<MetaInfo>().STag + ", "
                + pos[0] + ", " + pos[1]);
        }

        public GameObject NextTurn()
        {
            EndTurn(GetComponent<Schedule>().Current);
            StartTurn(GetComponent<Schedule>().GotoNext());

            return GetComponent<Schedule>().Current;
        }

        public void StartTurn(GameObject actor)
        {
            int[] pos = GetComponent<ConvertCoordinate>().Convert(
               actor.transform.position);
            Debug.Log("Start: " + actor.GetComponent<MetaInfo>().STag + ", "
                + pos[0] + ", " + pos[1]);
        }
    }
}
