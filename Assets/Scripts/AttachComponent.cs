using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PrototypeFactory;
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
            gameObject.AddComponent<Blueprint>();
            gameObject.AddComponent<BlueprintAltar>();
            gameObject.AddComponent<BlueprintFloor>();

            gameObject.AddComponent<ConvertCoordinate>();
            gameObject.AddComponent<CreateObject>();
            gameObject.AddComponent<DungeonBoard>();
            gameObject.AddComponent<GameCore>();

            gameObject.AddComponent<ObjectPool>();
            gameObject.AddComponent<RemoveObject>();
            gameObject.AddComponent<SearchObject>();
            gameObject.AddComponent<Wizard>();
        }
    }
}
