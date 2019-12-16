using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.ObjectFactory;
using AxeMan.GameSystem.PlayerInput;
using AxeMan.GameSystem.PrototypeFactory;
using AxeMan.GameSystem.SaveLoadGameFile;
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
            gameObject.AddComponent<InitializeMainGame>().enabled = false;

            gameObject.AddComponent<ActorComponent>();
            gameObject.AddComponent<ActorData>();
            gameObject.AddComponent<AimMarkerComponent>();
            gameObject.AddComponent<AimMarkerInputManager>();
            gameObject.AddComponent<AimMode>();

            gameObject.AddComponent<AltarColor>();
            gameObject.AddComponent<AltarCooldown>();
            gameObject.AddComponent<AltarSprite>();
            gameObject.AddComponent<AnnouncePCDeath>();

            gameObject.AddComponent<Blueprint>();
            gameObject.AddComponent<BlueprintActor>();
            gameObject.AddComponent<BlueprintAimMarker>();
            gameObject.AddComponent<BlueprintAltar>();
            gameObject.AddComponent<BlueprintExamineMarker>();

            gameObject.AddComponent<BlueprintFloor>();
            gameObject.AddComponent<BlueprintProgressBar>();
            gameObject.AddComponent<BlueprintTrap>();

            gameObject.AddComponent<BuildingEffect>();
            gameObject.AddComponent<BuryNPC>();
            gameObject.AddComponent<BuryPC>();

            gameObject.AddComponent<Canvas_ExamineMode>();
            gameObject.AddComponent<Canvas_Help>();
            gameObject.AddComponent<Canvas_Log>();
            gameObject.AddComponent<Canvas_Message>();

            gameObject.AddComponent<Canvas_PCStatus>();
            gameObject.AddComponent<Canvas_PCStatus_CurrentStatus>();
            gameObject.AddComponent<Canvas_PCStatus_HPSkill>();
            gameObject.AddComponent<Canvas_PCStatus_SkillData>();
            gameObject.AddComponent<Canvas_PCStatus_SkillFlawEffect>();

            gameObject.AddComponent<Canvas_World>();
            gameObject.AddComponent<ColorData>();
            gameObject.AddComponent<ColorManager>();
            gameObject.AddComponent<ConfirmCancelInput>();

            gameObject.AddComponent<ConvertCoordinate>();
            gameObject.AddComponent<ConvertSkillMetaInfo>();
            gameObject.AddComponent<CreateObject>();

            gameObject.AddComponent<DeadMode>();
            gameObject.AddComponent<DeadPCInput>();
            gameObject.AddComponent<DeadPCInputManager>();
            gameObject.AddComponent<Distance>();
            gameObject.AddComponent<DungeonBoard>();
            gameObject.AddComponent<DungeonObjectComponent>();

            gameObject.AddComponent<ExamineInput>();
            gameObject.AddComponent<ExamineMarkerComponent>();
            gameObject.AddComponent<ExamineMarkerInputManager>();
            gameObject.AddComponent<ExamineMode>();

            gameObject.AddComponent<GameCore>();
            gameObject.AddComponent<GameModeManager>();
            gameObject.AddComponent<GameVersion>();
            gameObject.AddComponent<InitializeStartScreen>();
            gameObject.AddComponent<InputManager>();

            gameObject.AddComponent<LogData>();
            gameObject.AddComponent<LogInput>();
            gameObject.AddComponent<LogManager>();
            gameObject.AddComponent<LogMarkerInputManager>();
            gameObject.AddComponent<LogMode>();

            gameObject.AddComponent<MarkerPosition>();
            gameObject.AddComponent<MovementInput>();
            gameObject.AddComponent<NormalMode>();
            gameObject.AddComponent<NPCComponent>();

            gameObject.AddComponent<ObjectPool>();
            gameObject.AddComponent<PathFinding>();
            gameObject.AddComponent<PCComponent>();
            gameObject.AddComponent<PCInputManager>();
            gameObject.AddComponent<ProgressBar>();

            gameObject.AddComponent<PublishAction>();
            gameObject.AddComponent<PublishActorStatus>();
            gameObject.AddComponent<PublishActorHP>();
            gameObject.AddComponent<PublishPosition>();
            gameObject.AddComponent<PublishSkill>();

            gameObject.AddComponent<RemoveObject>();
            gameObject.AddComponent<SaveLoadXML>();
            gameObject.AddComponent<Schedule>();

            gameObject.AddComponent<SearchObject>();
            gameObject.AddComponent<SearchUI>();
            gameObject.AddComponent<SettingData>();

            gameObject.AddComponent<SkillData>();
            gameObject.AddComponent<SkillInput>();
            gameObject.AddComponent<SkillTemplateData>();
            gameObject.AddComponent<StartScreen>();
            gameObject.AddComponent<StartScreenInputManager>();

            gameObject.AddComponent<TileOverlay>();
            gameObject.AddComponent<TurnManager>();
            gameObject.AddComponent<UITextData>();
            gameObject.AddComponent<UIManager>();
            gameObject.AddComponent<UpgradeAltar>();

            gameObject.AddComponent<VerifySkillCooldown>();
            gameObject.AddComponent<VerifySkillRange>();
            gameObject.AddComponent<VerifySkillTarget>();

            gameObject.AddComponent<Wizard>();
            gameObject.AddComponent<WizardInput>();
        }
    }
}
