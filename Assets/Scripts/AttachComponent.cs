using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PlayerInput;
using AxeMan.GameSystem.PrototypeFactory;
using AxeMan.GameSystem.SchedulingSystem;
using AxeMan.GameSystem.SearchGameObject;
using AxeMan.GameSystem.UserInterface;
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
            gameObject.AddComponent<ActorComponent>();
            gameObject.AddComponent<AimMarkerComponent>();
            gameObject.AddComponent<AimMode>();

            gameObject.AddComponent<Blueprint>();
            gameObject.AddComponent<BlueprintActor>();
            gameObject.AddComponent<BlueprintAimMarker>();

            gameObject.AddComponent<BlueprintAltar>();
            gameObject.AddComponent<BlueprintFloor>();
            gameObject.AddComponent<BlueprintProgressBar>();
            gameObject.AddComponent<BlueprintTrap>();

            gameObject.AddComponent<Canvas_PCStatus_Left>();
            gameObject.AddComponent<Canvas_PCStatus_Middle>();

            gameObject.AddComponent<ConfirmCancelInput>();
            gameObject.AddComponent<ConvertCoordinate>();
            gameObject.AddComponent<CreateObject>();

            gameObject.AddComponent<DungeonBoard>();
            gameObject.AddComponent<GameCore>();
            gameObject.AddComponent<InputManager>();

            gameObject.AddComponent<MovementInput>();
            gameObject.AddComponent<NPCComponent>();
            gameObject.AddComponent<ObjectPool>();

            gameObject.AddComponent<PCComponent>();
            gameObject.AddComponent<ProgressBar>();
            gameObject.AddComponent<PublishAction>();
            gameObject.AddComponent<PublishHP>();
            gameObject.AddComponent<PublishPosition>();

            gameObject.AddComponent<RemoveObject>();
            gameObject.AddComponent<Schedule>();
            gameObject.AddComponent<SearchObject>();
            gameObject.AddComponent<SearchUI>();
            gameObject.AddComponent<SkillInput>();

            gameObject.AddComponent<TileOverlay>();
            gameObject.AddComponent<TurnManager>();

            gameObject.AddComponent<UIManager>();
            gameObject.AddComponent<Wizard>();
            gameObject.AddComponent<WizardInput>();
        }
    }
}
