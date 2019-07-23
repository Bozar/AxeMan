using AxeMan.GameSystem;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.ObjectFactory;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface ILocalManager
    {
        int GetDistance(int[] target);

        bool IsPassable(int[] position);

        bool MatchID(int id);

        void Remove();

        void SetPosition(int[] position);

        void TakenAction(TakenActionEventArgs e);

        void TakingAction(TakingActionEventArgs e);
    }

    public class LocalManager : MonoBehaviour, ILocalManager
    {
        public int GetDistance(int[] target)
        {
            return GameCore.AxeManCore.GetComponent<Distance>()
                .GetDistance(GetComponent<MetaInfo>().Position, target);
        }

        public bool IsPassable(int[] position)
        {
            CheckingTerrainEventArgs e = new CheckingTerrainEventArgs(
               gameObject.GetInstanceID(), position, new Stack<bool>());
            GameCore.AxeManCore.GetComponent<PublishPosition>().CheckTerrain(e);

            while (e.IsPassable.Count > 0)
            {
                if (!e.IsPassable.Pop())
                {
                    return false;
                }
            }
            return true;
        }

        public bool MatchID(int id)
        {
            return gameObject.GetInstanceID() == id;
        }

        public void Remove()
        {
            GameCore.AxeManCore.GetComponent<RemoveObject>().Remove(gameObject);
        }

        public void SetPosition(int[] position)
        {
            transform.position
                = GameCore.AxeManCore.GetComponent<ConvertCoordinate>()
                .Convert(position);
        }

        public void TakenAction(TakenActionEventArgs e)
        {
            GameCore.AxeManCore.GetComponent<PublishAction>()
               .ActorTakenAction(e);
        }

        public void TakingAction(TakingActionEventArgs e)
        {
            GameCore.AxeManCore.GetComponent<PublishAction>()
                .ActorTakingAction(e);
        }
    }
}
