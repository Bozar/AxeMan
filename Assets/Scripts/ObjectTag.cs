using UnityEngine;

namespace AxeMan.GameSystem
{
    public enum BlueprintTag { INVALID, Altar, Floor, Trap, }

    public enum MainTag { INVALID, Building, Trap, Actor, Floor, }

    public enum SearchEventTag { Position, MainTag, SubTag, }

    public enum SubTag
    {
        INVALID, Dummy, LifeAltar, ShieldAltar, Floor,
        FireTrap, IceTrap, LightningTrap, EarthTrap,
    }

    public class ObjectTag : MonoBehaviour
    {
    }
}
