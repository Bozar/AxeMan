using UnityEngine;

namespace AxeMan.GameSystem.GameDataTag
{
    public enum ActionTag
    {
        INVALID, Skip, Move, UseSkillQ, UseSkillW, UseSkillE, UseSkillR,
    }

    public enum BlueprintTag
    {
        INVALID, Altar, Floor, Trap, Actor, ProgressBar, AimMarker,
    }

    public enum CanvasTag
    {
        Canvas_World,
        Canvas_PCStatus_Left, Canvas_PCStatus_Middle, Canvas_PCStatus_Right,
    }

    public enum CommandTag
    {
        INVALID, Confirm, Cancel,
        Test, Reload, PrintSchedule, RemoveFromSchedule, NextInSchedule,
        ChangeHP, PrintSkill,
        Left, Right, Up, Down,
        SkillQ, SkillW, SkillE, SkillR,
    }

    public enum MainTag { INVALID, Building, Trap, Actor, Floor, Marker, }

    public enum SearchEventTag { Position, MainTag, SubTag, }

    public enum SkillComponentTag
    {
        FireMerit, WaterMerit, AirMerit, EarthMerit,
        FireFlaw, WaterFlaw, AirFlaw, EarthFlaw,
        FireCurse, WaterCurse, AirCurse, EarthCurse,
    };

    public enum SkillNameTag { INVALID, Q, W, E, R }

    public enum SkillSlotTag { Merit1, Merit2, Merit3, Flaw1, Flaw2, Flaw3, }

    public enum SkillTypeTag { INVALID, Move, Attack, Buff, Curse, }

    public enum SubTag
    {
        INVALID, Dummy, LifeAltar, ShieldAltar, Floor, ProgressBar,
        FireTrap, IceTrap, LightningTrap, EarthTrap,
        PC, AimMarker,
    }

    public enum UITag
    {
        Modeline,
        HPText, HPData,
        QText, WText, EText, RText,
        QData, WData, EData, RData,
        QType, WType, EType, RType,
        SkillText, SkillData,
        Status1Text, Status1Data, Status2Text, Status2Data,
        Status3Text, Status3Data, Status4Text, Status4Data,
        RangeText, RangeData, CooldownText, CooldownData, DamageText, DamageData,
        Curse1Text, Curse1Data, Curse2Text, Curse2Data, Curse3Text, Curse3Data,
    }

    public class DataTag : MonoBehaviour
    {
    }
}
