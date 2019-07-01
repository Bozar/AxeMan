using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PlayerInput;
using AxeMan.GameSystem.PrototypeFactory;
using AxeMan.GameSystem.SchedulingSystem;
using AxeMan.GameSystem.SearchGameObject;
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
            gameObject.AddComponent<BlueprintActor>();
            gameObject.AddComponent<BlueprintAltar>();

            gameObject.AddComponent<BlueprintFloor>();
            gameObject.AddComponent<BlueprintProgressBar>();
            gameObject.AddComponent<BlueprintTrap>();

            gameObject.AddComponent<ConvertCoordinate>();
            gameObject.AddComponent<CreateObject>();
            gameObject.AddComponent<DungeonBoard>();
            gameObject.AddComponent<GameCore>();
            gameObject.AddComponent<InputManager>();

            gameObject.AddComponent<NPCComponent>();
            gameObject.AddComponent<ObjectPool>();
            gameObject.AddComponent<PCComponent>();
            gameObject.AddComponent<ProgressBar>();
            gameObject.AddComponent<PublishAction>();

            gameObject.AddComponent<RemoveObject>();
            gameObject.AddComponent<SearchObject>();
            gameObject.AddComponent<SearchUI>();

            gameObject.AddComponent<Schedule>();
            gameObject.AddComponent<TileOverlay>();
            gameObject.AddComponent<TurnManager>();
            gameObject.AddComponent<Wizard>();
        }
    }
}
