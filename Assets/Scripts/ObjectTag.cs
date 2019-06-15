using UnityEngine;

namespace AxeMan.GameSystem
{
    public enum BlueprintTag { INVALID, Altar, }

    public enum MainTag { INVALID, Building, Terrain, Actor }

    public enum SearchEventTag { Position, MainTag, SubTag, }

    public enum SubTag { INVALID, Dummy, LifeAltar, ShieldAltar, }

    public class ObjectTag : MonoBehaviour
    {
    }
}
