﻿using UnityEngine;

namespace AxeMan.GameSystem.GameDataTag
{
    public enum ActionTag
    {
        INVALID, Skip, Move, UseSkillQ, UseSkillW, UseSkillE, UseSkillR,
        ActiveAltar,
    }

    public enum ActorDataTag
    {
        Name, Cooldown, HP, PowerDuration, BuildingEffect, MoveDistance,
        AttackRange,
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

    public enum CommandTag
    {
        INVALID, Confirm, Cancel, ExamineMode,
        Test, Reload, PrintSchedule, RemoveFromSchedule, NextInSchedule,
        ChangeHP, PrintSkill,
        Left, Right, Up, Down,
        SkillQ, SkillW, SkillE, SkillR,
    }

    public enum LanguageTag { English, Chinese, }

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

    public enum SkillNameTag { INVALID, Q, W, E, R }

    public enum SkillSlotTag { Merit1, Merit2, Merit3, Flaw1, Flaw2, Flaw3, }

    public enum SkillTypeTag { INVALID, Move, Attack, Buff, Curse, }

    public enum SubTag
    {
        INVALID, DEFAULT, Dummy, Floor, ProgressBar,
        FireTrap, WaterTrap, AirTrap, EarthTrap,
        FireAltar, WaterAltar, AirAltar, EarthAltar, LifeAltar,
        PC, AimMarker, ExamineMarker, StartScreenCursor,
    }

    public enum UILabelDataTag { Cooldown, HP, MoveDistance, AttackRange, }

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

        Curse1Text, Curse2Text,
        Curse1Data, Curse2Data,

        MoveText, AttackText, DamageText,
        MoveData, AttackData, DamageData,
    }

    public class DataTag : MonoBehaviour
    {
    }
}
