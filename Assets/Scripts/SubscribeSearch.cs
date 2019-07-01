﻿using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.DungeonObject.GameEvent
{
    public class SubscribeSearch : MonoBehaviour
    {
        private bool MatchCriteria(int[] position)
        {
            int[] localPosition = GetComponent<LocalManager>().GetPosition();

            return (localPosition[0] == position[0])
                && (localPosition[1] == position[1]);
        }

        private bool MatchCriteria(SubTag sTag)
        {
            return GetComponent<MetaInfo>().STag == sTag;
        }

        private bool MatchCriteria(MainTag mTag)
        {
            return GetComponent<MetaInfo>().MTag == mTag;
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<SearchObject>().SearchingPosition
                += SubscribeSearch_SearchingPosition;
            GameCore.AxeManCore.GetComponent<SearchObject>().SearchingMainTag
                += SubscribeSearch_SearchingMainTag;
            GameCore.AxeManCore.GetComponent<SearchObject>().SearchingSubTag
                += SubscribeSearch_SearchingSubTag;
        }

        private void SubscribeSearch_SearchingMainTag(object sender,
            SearchingMainTagEventArgs e)
        {
            if (MatchCriteria(e.MTag))
            {
                e.Data.Push(gameObject);
            }
        }

        private void SubscribeSearch_SearchingPosition(object sender,
            SearchingPositionEventArgs e)
        {
            if (MatchCriteria(e.Position))
            {
                e.Data.Push(gameObject);
            }
        }

        private void SubscribeSearch_SearchingSubTag(object sender,
            SearchingSubTagEventArgs e)
        {
            if (MatchCriteria(e.STag))
            {
                e.Data.Push(gameObject);
            }
        }
    }
}