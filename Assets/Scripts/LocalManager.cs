using AxeMan.GameSystem;
using AxeMan.GameSystem.ObjectFactory;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface ILocalManager
    {
        int[] GetPosition();

        void Remove();
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
    }
}
