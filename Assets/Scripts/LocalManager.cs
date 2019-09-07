using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.ObjectFactory;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface ILocalManager
    {
        void CheckingSchedule(ActionTag actionTag);

        int GetDistance(int[] target);

        int[] GetRelativePosition(int[] target);

        bool IsPassable(int[] position);

        bool MatchID(int id);

        void Remove();

        void SetPosition(int[] position);

        void TakenAction(ActionTag actionTag);

        void TakingAction(ActionTag actionTag);
    }

    public class LocalManager : MonoBehaviour, ILocalManager
    {
        public void CheckingSchedule(ActionTag actionTag)
        {
            GameCore.AxeManCore.GetComponent<PublishAction>()
               .ActorCheckingSchedule(GetEventArg(actionTag));
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

        public void TakenAction(ActionTag actionTag)
        {
            GameCore.AxeManCore.GetComponent<PublishAction>()
               .ActorTakenAction(GetEventArg(actionTag));
        }

        public void TakingAction(ActionTag actionTag)
        {
            GameCore.AxeManCore.GetComponent<PublishAction>()
                .ActorTakingAction(GetEventArg(actionTag));
        }

        private PublishActionEventArgs GetEventArg(ActionTag actionTag)
        {
            MetaInfo metaInfo = GetComponent<MetaInfo>();
            MainTag mainTag = metaInfo.MainTag;
            SubTag subTag = metaInfo.SubTag;
            int id = metaInfo.ObjectID;

            return new PublishActionEventArgs(actionTag, mainTag, subTag, id);
        }
    }
}
