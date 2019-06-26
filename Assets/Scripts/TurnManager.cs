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
            throw new System.NotImplementedException();
        }

        public GameObject NextTurn()
        {
            throw new System.NotImplementedException();
        }

        public void StartTurn(GameObject actor)
        {
            throw new System.NotImplementedException();
        }
    }
}
