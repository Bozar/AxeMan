using AxeMan.GameSystem;
using AxeMan.GameSystem.GameEvent;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class NPCSetTrap : MonoBehaviour
    {
        private void NPCSetTrap_ChangingHP(object sender, ChangeHPEventArgs e)
        {
            if (e.IsAlive
                || (e.ID != GetComponent<MetaInfo>().ObjectID))
            {
                return;
            }
            // Test.
            int[] position = GetComponent<MetaInfo>().Position;
            Debug.Log("Dead: " + position[0] + "," + position[1]);
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PublishActorHP>().ChangingHP
                += NPCSetTrap_ChangingHP;
        }
    }
}
