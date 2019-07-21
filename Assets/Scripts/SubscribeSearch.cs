using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;

namespace AxeMan.DungeonObject.SearchGameObject
{
    public class SubscribeSearch : MonoBehaviour
    {
        private bool MatchCriteria(int[] position)
        {
            int[] localPosition = GetComponent<MetaInfo>().Position;

            return (localPosition[0] == position[0])
                && (localPosition[1] == position[1]);
        }

        private bool MatchCriteria(SubTag sTag)
        {
            return GetComponent<MetaInfo>().SubTag == sTag;
        }

        private bool MatchCriteria(MainTag mTag)
        {
            return GetComponent<MetaInfo>().MainTag == mTag;
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
            if (MatchCriteria(e.MainTag))
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
            if (MatchCriteria(e.SubTag))
            {
                e.Data.Push(gameObject);
            }
        }
    }
}
