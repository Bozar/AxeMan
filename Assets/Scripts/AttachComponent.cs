using AxeMan.GameSystem.Blueprint;
using AxeMan.GameSystem.ObjectFactory;
using UnityEngine;

// If a class is under the namespace `AxeMan.GameSystem`, it can be instanced
// only once and should be attached to the game object `AxeManCore`.
namespace AxeMan.GameSystem
{
    public class AttachComponent : MonoBehaviour
    {
        private void Awake()
        {
            // The `gameObject` is `AxeManCore`.
            gameObject.AddComponent<BlueprintCore>();
            gameObject.AddComponent<BpAltar>();
            gameObject.AddComponent<ConvertCoordinate>();
            gameObject.AddComponent<DungeonBoard>();

            gameObject.AddComponent<GameCore>();
            gameObject.AddComponent<ObjectFactoryCore>();
            gameObject.AddComponent<ObjectPool>();
            gameObject.AddComponent<OFBuilding>();

            gameObject.AddComponent<RemoveObject>();
            gameObject.AddComponent<Wizard>();
        }
    }
}
