using AxeMan.GameSystem;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.ObjectFactory;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface ILocalManager
    {
        void CheckingSchedule(PublishActionEventArgs e);

        int GetDistance(int[] target);

        int[] GetRelativePosition(int[] target);

        bool IsPassable(int[] position);

        bool MatchID(int id);

        void Remove();

        void SetPosition(int[] position);

        void TakenAction(PublishActionEventArgs e);

        void TakingAction(PublishActionEventArgs e);
    }

    public class LocalManager : MonoBehaviour, ILocalManager
    {
        public void CheckingSchedule(PublishActionEventArgs e)
        {
            GameCore.AxeManCore.GetComponent<PublishAction>()
               .ActorCheckingSchedule(e);
        }

        public int GetDistance(int[] target)
        {
            return GameCore.AxeManCore.GetComponent<Distance>()
                .GetDistance(GetComponent<MetaInfo>().Position, target);
        }

        public int[] GetRelativePosition(int[] target)
        {
            return GameCore.AxeManCore.GetComponent<Distance>()
                .GetRelativePosition(GetComponent<MetaInfo>().Position, target);
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

        public void TakenAction(PublishActionEventArgs e)
        {
            GameCore.AxeManCore.GetComponent<PublishAction>()
               .ActorTakenAction(e);
        }

        public void TakingAction(PublishActionEventArgs e)
        {
            GameCore.AxeManCore.GetComponent<PublishAction>()
                .ActorTakingAction(e);
        }
    }
}
