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
        AttackRange, Damage, CurseEffect, CurseData, MaxUpgrade, MaxDistance,
        AddCooldown, AddPowerDuration, BumpDamage, RestoreHP, SetTrap,
    }

    public enum BlueprintTag
    {
        INVALID, Altar, Floor, Trap, Actor, ProgressBar,
        AimMarker, ExamineMarker, StartScreenCursor,
    }

    public enum CanvasTag
    {
        Canvas_Main, Canvas_Start,
        Canvas_World, Canvas_Message, Canvas_ExamineMode,
        Canvas_PCStatus_HPSkill, Canvas_PCStatus_CurrentStatus,
        Canvas_PCStatus_SkillData, Canvas_PCStatus_SkillFlawEffect,
    }

    public enum ColorTag
    {
        INVALID, White, Black, Grey, Orange, Green,
    }

    public enum CommandTag
    {
        INVALID, Confirm, Cancel, ExamineMode,
        Test, ForceReload, Reload, PrintSchedule, RemoveFromSchedule,
        NextInSchedule,
        ChangeHP, PrintSkill,
        Left, Right, Up, Down,
        SkillQ, SkillW, SkillE, SkillR,
    }

    public enum LanguageTag { English, Chinese, }

    public enum LogCategoryTag
    {
        INVALID, Combat, Altar, Trap,
    }

    public enum LogMessageTag
    {
        INVALID,
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
    }

    public enum SkillTypeTag { INVALID, Move, Attack, Buff, Curse, }

    public enum SubTag
    {
        INVALID, DEFAULT, Dummy, Floor, ProgressBar,
        FireTrap, WaterTrap, AirTrap, EarthTrap,
        FireAltar, WaterAltar, AirAltar, EarthAltar, LifeAltar,
        PC, AimMarker, ExamineMarker, StartScreenCursor,
    }

    public enum UILabelDataTag
    {
        Cooldown, HP, MoveDistance, AttackRange, Damage,
    }

    public enum UITag
    {
        Modeline, UIText,

        HPText, SkillText, RangeText, CooldownText,
        HPData, SkillData, RangeData, CooldownData,

        QText, WText, EText, RText,
        QData, WData, EData, RData,
        QType, WType, EType, RType,

        Status1Text, Status2Text, Status3Text, Status4Text,
        Status1Data, Status2Data, Status3Data, Status4Data,

        MoveText, AttackText, DamageText, CurseText,
        MoveData, AttackData, DamageData, CurseData,

        Line1, Line2, Line3, Line4, Line5, Line6,
    }

    public class DataTag : MonoBehaviour
    {
    }
}
