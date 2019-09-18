using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataHub;
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
        private int maxDistance;
        private int maxUpgrade;
        private MetaInfo pcMetaInfo;
        private int upgradeCount;

        public event EventHandler<EventArgs> UpgradingAltar;

        protected virtual void OnUpgradingAltar(EventArgs e)
        {
            UpgradingAltar?.Invoke(this, e);
        }

        private void Awake()
        {
            upgradeCount = 0;
        }

        private bool CheckDistance(int[] position)
        {
            return GetComponent<Distance>().GetDistance(
                position, pcMetaInfo.Position)
                < maxDistance;
        }

        private bool CheckUpgrade()
        {
            return upgradeCount < maxUpgrade;
        }

        private void SetAltarPositions()
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

        private void SetMaximums()
        {
            maxUpgrade = GetComponent<ActorData>().GetIntData(
                MainTag.Altar, SubTag.DEFAULT, ActorDataTag.MaxUpgrade);
            maxDistance = GetComponent<ActorData>().GetIntData(
                MainTag.Altar, SubTag.DEFAULT, ActorDataTag.MaxDistance);
        }

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

            foreach (int[] pos in altarPositions)
            {
                if (CheckDistance(pos) && CheckUpgrade())
                {
                    OnUpgradingAltar(EventArgs.Empty);
                    upgradeCount++;
                    break;
                }
            }
        }

        private void UpgradeAltar_CreatedWorld(object sender, EventArgs e)
        {
            SetAltarPositions();
            SetMaximums();
        }

        private void UpgradeAltar_SettingReference(object sender,
            SettingReferenceEventArgs e)
        {
            pcMetaInfo = e.PC.GetComponent<MetaInfo>();
        }
    }
}
