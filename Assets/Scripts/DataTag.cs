using UnityEngine;

namespace AxeMan.GameSystem.GameDataTag
{
    public enum ActionTag { INVALID, Skip, }

    public enum BlueprintTag { INVALID, Altar, Floor, Trap, Actor, }

    public enum CanvasTag { Canvas_World, }

    public enum CommandTag
    {
        INVALID, Test, Reload, PrintSchedule, RemoveFromSchedule, NextInSchedule,
        Left, Right, Up, Down,
    }

    public enum MainTag { INVALID, Building, Trap, Actor, Floor, }

    public enum SearchEventTag { Position, MainTag, SubTag, }

    public enum SubTag
    {
        INVALID, Dummy, LifeAltar, ShieldAltar, Floor,
        FireTrap, IceTrap, LightningTrap, EarthTrap,
        PC,
    }

    public enum UITag { Modeline, }

    public class DataTag : MonoBehaviour
    {
    }
}
