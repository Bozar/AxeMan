using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.SchedulingSystem;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class BuryNPC : MonoBehaviour
    {
        private void BuryNPC_ChangedHP(object sender, ChangedHPEventArgs e)
        {
            if (e.IsAlive || (e.SubTag == SubTag.PC))
            {
                return;
            }
            if (!GetComponent<SearchObject>().Search(e.ID,
                out GameObject[] actors))
            {
                return;
            }

            GameObject actor = actors[0];
            actor.GetComponent<LocalManager>().Remove();
            GetComponent<TurnManager>().StartTurn();
        }

        private void Start()
        {
            GetComponent<PublishHP>().ChangedHP += BuryNPC_ChangedHP;
        }
    }
}
