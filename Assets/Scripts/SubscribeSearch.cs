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

        private bool MatchCriteria(SubTag subTag)
        {
            return GetComponent<MetaInfo>().SubTag == subTag;
        }

        private bool MatchCriteria(MainTag mainTag)
        {
            return GetComponent<MetaInfo>().MainTag == mainTag;
        }

        private bool MatchCriteria(int objectID)
        {
            return GetComponent<MetaInfo>().ObjectID == objectID;
        }

        private void Start()
        {
            SearchObject so = GameCore.AxeManCore.GetComponent<SearchObject>();

            so.SearchingPosition += SubscribeSearch_SearchingPosition;
            so.SearchingMainTag += SubscribeSearch_SearchingMainTag;
            so.SearchingSubTag += SubscribeSearch_SearchingSubTag;
            so.SearchingID += SubscribeSearch_SearchingID;
        }

        private void SubscribeSearch_SearchingID(object sender,
            SearchingIDEventArgs e)
        {
            if (MatchCriteria(e.ObjectID))
            {
                e.Data.Push(gameObject);
            }
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
