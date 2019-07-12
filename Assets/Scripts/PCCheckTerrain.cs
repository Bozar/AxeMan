using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCCheckTerrain : MonoBehaviour
    {
        private void PCCheckTerrain_CheckingTerrain(object sender,
            CheckingTerrainEventArgs e)
        {
            if (e.ObjectID != gameObject.GetInstanceID())
            {
                return;
            }
            e.IsPassable.Push(GameCore.AxeManCore.GetComponent<SearchObject>()
                .Search(e.Position[0], e.Position[1], SubTag.Floor, out _));
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PublishPosition>().CheckingTerrain
                += PCCheckTerrain_CheckingTerrain;
        }
    }
}
