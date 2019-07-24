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
        Test, Reload, PrintSchedule, RemoveFromSchedule, NextInSchedule, ChangeHP,
        Left, Right, Up, Down,
        SkillQ, SkillW, SkillE, SkillR,
    }

    public enum MainTag { INVALID, Building, Trap, Actor, Floor, Marker, }

    public enum SearchEventTag { Position, MainTag, SubTag, }

    public enum SkillComponentTag { FireShield, };

    public enum SkillNameTag { INVALID, Q, W, E, R }

    public enum SkillTypeTag { INVALID, Move, Attack }

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
        SkillText, SkillData, FireText, FireData, WaterText, WaterData,
        AirText, AirData, EarthText, EarthData,
        RangeText, RangeData, CooldownText, CooldownData, DamageText, DamageData,
        Curse1Text, Curse1Data, Curse2Text, Curse2Data, Curse3Text, Curse3Data,
    }

    public class DataTag : MonoBehaviour
    {
    }
}
