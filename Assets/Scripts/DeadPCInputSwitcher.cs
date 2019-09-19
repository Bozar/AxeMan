using AxeMan.GameSystem;
using System;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class DeadPCInputSwitcher : MonoBehaviour
    {
        private void DeadPCInputSwitcher_BuryingPC(object sender, EventArgs e)
        {
            GetComponent<DeadPCInputManager>().enabled = true;
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<BuryPC>().BuryingPC
                += DeadPCInputSwitcher_BuryingPC;
        }
    }
}
