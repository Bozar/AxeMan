using AxeMan.GameSystem;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.ObjectFactory;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface ILocalManager
    {
        int[] Position { get; }

        bool IsPassable(int[] position);

        void Remove();

        void SetPosition(int[] position);

        void TakenAction(TakenActionEventArgs e);

        void TakingAction(TakingActionEventArgs e);
    }

    public class LocalManager : MonoBehaviour, ILocalManager
    {
        public int[] Position
        {
            get
            {
                return GameCore.AxeManCore.GetComponent<ConvertCoordinate>()
                    .Convert(transform.position);
            }
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
