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
        private void BuryNPC_ChangedHP(object sender, ChangeHPEventArgs e)
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
            bool isCurrentActor = (actor == GetComponent<Schedule>().Current);
            int[] position = actor.GetComponent<MetaInfo>().Position;

            actor.GetComponent<LocalManager>().Remove();
            GetComponent<TileOverlay>().TryHideTile(position);

            // Call StartTurn() manually only when an active actor kills himself.
            // Otherwise StartTurn() will be called implicitly.
            if (isCurrentActor)
            {
                GetComponent<TurnManager>().StartTurn();
            }
        }

        private void Start()
        {
            GetComponent<PublishActorHP>().ChangedHP += BuryNPC_ChangedHP;
        }
    }
}
