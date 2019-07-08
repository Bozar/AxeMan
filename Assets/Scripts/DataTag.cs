using UnityEngine;

namespace AxeMan.GameSystem.GameDataTag
{
    public enum ActionTag { INVALID, Skip, Move, }

    public enum BlueprintTag { INVALID, Altar, Floor, Trap, Actor, ProgressBar, }

    public enum CanvasTag
    {
        Canvas_World,
        Canvas_PCStatus_Left, Canvas_PCStatus_Middle,
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

    public enum SkillNameTag { Q, W, E, R }

    public enum SkillTypeTag { Move, Attack }

    public enum SubTag
    {
        INVALID, Dummy, LifeAltar, ShieldAltar, Floor, ProgressBar,
        FireTrap, IceTrap, LightningTrap, EarthTrap,
        PC,
    }

    public enum UITag
    {
        Modeline,
        HPText, HPData, QText, QData, WText, WData, EText, EData, RText, RData,
    }

    public class DataTag : MonoBehaviour
    {
    }
}
