using AxeMan.GameSystem;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.ObjectFactory;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface ILocalManager
    {
        int[] GetPosition();

        void Remove();

        void SetPosition(int[] position);

        void TakenAction(TakenActionEventArgs e);

        void TakingAction(TakingActionEventArgs e);
    }

    public class LocalManager : MonoBehaviour, ILocalManager
    {
        public int[] GetPosition()
        {
            return GameCore.AxeManCore.GetComponent<ConvertCoordinate>()
                .Convert(transform.position);
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
