using AxeMan.GameSystem;
using AxeMan.GameSystem.ObjectFactory;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface ILocalManager
    {
        void Remove();
    }

    public class LocalManager : MonoBehaviour, ILocalManager
    {
        public void Remove()
        {
            GameCore.AxeManCore.GetComponent<RemoveObject>().Remove(gameObject);
        }
    }
}
