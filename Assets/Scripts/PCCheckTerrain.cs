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
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            bool isPassable = false;
            GameCore.AxeManCore.GetComponent<SearchObject>()
               .Search(e.Position[0], e.Position[1], out GameObject[] targets);

            foreach (GameObject go in targets)
            {
                if ((go.GetComponent<MetaInfo>().MainTag == MainTag.Actor)
                    || (go.GetComponent<MetaInfo>().MainTag == MainTag.Altar))
                {
                    isPassable = false;
                    break;
                }
                else if (go.GetComponent<MetaInfo>().MainTag == MainTag.Floor)
                {
                    isPassable = true;
                }
            }
            e.IsPassable.Push(isPassable);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PublishPosition>().CheckingTerrain
                += PCCheckTerrain_CheckingTerrain;
        }
    }
}
