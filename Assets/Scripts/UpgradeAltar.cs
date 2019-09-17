using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SearchGameObject;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public class UpgradeAltar : MonoBehaviour
    {
        private int[][] altarPositions;
        private GameObject pc;

        private void Start()
        {
            GetComponent<InitializeMainGame>().SettingReference
                += UpgradeAltar_SettingReference;
            GetComponent<InitializeMainGame>().CreatedWorld
                += UpgradeAltar_CreatedWorld;
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
            Debug.Log("==========");
            foreach (int[] p in altarPositions)
            {
                Debug.Log(p[0] + "," + p[1]);
            }
        }

        private void UpgradeAltar_CreatedWorld(object sender, EventArgs e)
        {
            GameObject[] altars = GetComponent<SearchObject>().Search(
                MainTag.Altar);
            Stack<int[]> position = new Stack<int[]>();

            foreach (GameObject go in altars)
            {
                position.Push(go.GetComponent<MetaInfo>().Position);
            }
            altarPositions = position.ToArray();
        }

        private void UpgradeAltar_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            pc = e.PC;
        }
    }
}
