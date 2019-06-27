using AxeMan.GameSystem;
using UnityEngine;

namespace AxeMan.DungeonObject.GameEvent
{
    public class SubscribeSearchEvent : MonoBehaviour
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
                += SubscribeSearchEvent_SearchingPosition;
            GameCore.AxeManCore.GetComponent<SearchObject>().SearchingMainTag
                += SubscribeSearchEvent_SearchingMainTag;
            GameCore.AxeManCore.GetComponent<SearchObject>().SearchingSubTag
                += SubscribeSearchEvent_SearchingSubTag;
        }

        private void SubscribeSearchEvent_SearchingMainTag(object sender,
            SearchingMainTagEventArgs e)
        {
            if (MatchCriteria(e.MTag))
            {
                e.Data.Push(gameObject);
            }
        }

        private void SubscribeSearchEvent_SearchingPosition(object sender,
            SearchingPositionEventArgs e)
        {
            if (MatchCriteria(e.Position))
            {
                e.Data.Push(gameObject);
            }
        }

        private void SubscribeSearchEvent_SearchingSubTag(object sender,
            SearchingSubTagEventArgs e)
        {
            if (MatchCriteria(e.STag))
            {
                e.Data.Push(gameObject);
            }
        }
    }
}
