using AxeMan.GameSystem;
using AxeMan.GameSystem.ObjectFactory;
using UnityEngine;

namespace AxeMan.Actor
{
    public interface ILocalManager
    {
        int[] GetPosition();

        void Remove();
    }

    public class LocalManager : MonoBehaviour, ILocalManager
    {
        public int[] GetPosition()
        {
            return GameCore.AxeManCore.GetComponent<ConvertCoordinate>()
                .Convert(transform.position);
        }

        public void Remove()
        {
            GameCore.AxeManCore.GetComponent<RemoveObject>().Remove(gameObject);
        }

        private void LocalManager_SearchingMainTag(object sender,
            SearchingMainTagEventArgs e)
        {
            if (MatchCriteria(e.MTag))
            {
                e.Data.Push(gameObject);
            }
        }

        private void LocalManager_SearchingPosition(object sender,
            SearchingPositionEventArgs e)
        {
            if (MatchCriteria(e.Position))
            {
                e.Data.Push(gameObject);
            }
        }

        private void LocalManager_SearchingSubTag(object sender,
            SearchingSubTagEventArgs e)
        {
            if (MatchCriteria(e.STag))
            {
                e.Data.Push(gameObject);
            }
        }

        private bool MatchCriteria(int[] position)
        {
            int[] localPosition = GetPosition();

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
                += LocalManager_SearchingPosition;
            GameCore.AxeManCore.GetComponent<SearchObject>().SearchingMainTag
                += LocalManager_SearchingMainTag;
            GameCore.AxeManCore.GetComponent<SearchObject>().SearchingSubTag
                += LocalManager_SearchingSubTag;
        }
    }
}
