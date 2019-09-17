using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.InitializeGameWorld;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class UpgradeAltar : MonoBehaviour
    {
        private GameObject pc;

        private void Start()
        {
            GetComponent<InitializeMainGame>().SettingReference
                += UpgradeAltar_SettingReference;
            GetComponent<PublishHP>().ChangedHP += UpgradeAltar_ChangedHP;
        }

        private void UpgradeAltar_ChangedHP(object sender, ChangedHPEventArgs e)
        {
            if (e.IsAlive || (e.SubTag == SubTag.PC))
            {
                return;
            }

            int[] pos = pc.GetComponent<MetaInfo>().Position;
            Debug.Log(pos[0] + "," + pos[1]);
        }

        private void UpgradeAltar_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            pc = e.PC;
        }
    }
}
