using UnityEngine;

namespace AxeMan.GameSystem.GameDataTag
{
    public enum ActionTag
    {
        INVALID, Skip, Move, UseSkillQ, UseSkillW, UseSkillE, UseSkillR,
        ActiveAltar, BumpAttack, NPCAttack, NPCFindPath,
    }

    public enum ActorDataTag
    {
        Name, Cooldown, HP, PowerDuration, BuildingEffect, MoveDistance,
        AttackRange, Damage, CurseEffect, CurseData, MaxLevel, MaxDistance,
        AddCooldown, AddPowerDuration, BumpDamage, RestoreHP, SetTrap,
    }

    public enum BlueprintTag
    {
        INVALID, Altar, Floor, Trap, Actor, ProgressBar,
        AimMarker, ExamineMarker, StartScreenCursor, LogMarker,
    }

    public enum CanvasTag
    {
        Canvas_Main, Canvas_Start, Canvas_World, Canvas_Log, Canvas_BuildSkill,
        Canvas_BuildSkill_Footer, Canvas_BuildSkill_Left,
        Canvas_BuildSkill_Middle, Canvas_BuildSkill_Right,
        Canvas_Message, Canvas_ExamineMode, Canvas_Help, Canvas_PCStatus,
        Canvas_PCStatus_HPSkill, Canvas_PCStatus_CurrentStatus,
        Canvas_PCStatus_SkillData, Canvas_PCStatus_SkillFlawEffect,
    }

    public enum ColorTag
    {
        INVALID, White, Black, Grey, Orange, Green,
    }

    public enum CommandTag
    {
        INVALID, Confirm, Cancel, ExamineMode, LogMode,
        Test, ForceReload, Reload, PrintSchedule, RemoveFromSchedule,
        NextInSchedule,
        ChangeHP, PrintSkill,
        Left, Right, Up, Down,
        SkillQ, SkillW, SkillE, SkillR,
    }

    public enum GameModeTag
    {
        INVALID, NormalMode, ExamineMode, AimMode, LogMode, DeadMode, StartMode,
        BuildSkillMode,
    }

    public enum LanguageTag { English, Chinese, }

    public enum LogCategoryTag
    {
        INVALID, Combat, Altar, Trap, GameProgress,
    }

    public enum LogMessageTag
    {
        INVALID, NewTurn, PCDeath,
        PCHit, PCCurse, NPCHit, NPCCurse,
        PCTriggerTrap, NPCTriggerTrap,
        PCBuff, PCTeleport,
        UpgradeAltar, ActivateAltar,
    }

    public enum MainTag { INVALID, Altar, Trap, Actor, Floor, Marker, }

    public enum SearchEventTag { Position, MainTag, SubTag, }

    public enum SettingDataTag { ShowStartMenu, Language, }

    public enum SkillComponentTag
    {
        INVALID, Life,
        FireMerit, WaterMerit, AirMerit, EarthMerit,
        FireFlaw, WaterFlaw, AirFlaw, EarthFlaw,
        FireCurse, WaterCurse, AirCurse, EarthCurse,
    };

    public enum SkillNameTag { INVALID, SkillQ, SkillW, SkillE, SkillR }

    public enum SkillSlotTag
    {
        SkillType, Merit1, Merit2, Merit3, Flaw1, Flaw2, Flaw3,
        Component1, Component2, Component3, Component4, Component5
    }

    public enum SkillTypeTag { INVALID, Move, Attack, Buff, Curse, }

    public enum SubTag
    {
        INVALID, DEFAULT, Dummy, Floor, ProgressBar,
        FireTrap, WaterTrap, AirTrap, EarthTrap,
        FireAltar, WaterAltar, AirAltar, EarthAltar, LifeAltar,
        PC, AimMarker, ExamineMarker, LogMarker,
    }

    public enum UITag
    {
        Modeline, UIText,

        HPText, SkillText, RangeText, CooldownText,
        HPData, SkillData, RangeData, CooldownData,

        QText, WText, EText, RText,
        QData, WData, EData, RData,
        QType, WType, EType, RType,

        Status1Text, Status2Text, Status3Text, Status4Text, Status5Text,
        Status1Data, Status2Data, Status3Data, Status4Data, Status5Data,

        Status6Text, Status7Text, Status8Text, Status9Text,
        Status6Data, Status7Data, Status8Data, Status9Data,

        MoveText, AttackText, DamageText, CurseText,
        MoveData, AttackData, DamageData, CurseData,

        Line1, Line2, Line3, Line4, Line5,
        Line6, Line7, Line8, Line9, Line10,
        Line11, Line12, Line13, Line14, Line15,
        Line16, Line17, Line18, Line19, Line20,

        Text1, Text2, Text3, Text4, Text5,
        Text6, Text7, Text8, Text9, Text10,
        Text11, Text12, Text13, Text14, Text15,
        Text16, Text17, Text18, Text19, Text20,
    }

    public enum UITextCategoryTag
    {
        ActorStatus, Log, Help, World, BuildSkill,
    }

    public enum UITextDataTag
    {
        Cooldown, HP, MoveDistance, AttackRange, Damage, Hint, SkillType,
        NormalMode, AimMode, ExamineMode, MovePC, EnterExamine, EnterAim,
        ViewLog, Save, Load, MoveCursor, UseSkill, SwitchSkill, ExitMode,
        Version, Seed, Difficulty, GameProgress, AltarLevel, AltarCooldown,
        Merit1, Merit2, Merit3, Merit4, Flaw1, Flaw2, Flaw3, Flaw4,
    }

    public class DataTag : MonoBehaviour
    {
    }
}
