using AxeMan.GameSystem;
using AxeMan.GameSystem.ObjectFactory;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public interface ILocalManager
    {
        int[] GetPosition();

        void Remove();

        void Search(object sender, SearchingObjectEventArgs e);
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

        public void Search(object sender, SearchingObjectEventArgs e)
        {
            LocalManager_SearchingObject(sender, e);
        }

        private void LocalManager_SearchingObject(object sender,
            SearchingObjectEventArgs e)
        {
            bool match = false;

            switch (e.SearchTag)
            {
                case SearchEventTag.Position:
                    match = MatchCriteria(e.Position);
                    break;

                case SearchEventTag.MainTag:
                    match = MatchCriteria(e.MTag);
                    break;

                case SearchEventTag.SubTag:
                    match = MatchCriteria(e.STag);
                    break;

                default:
                    break;
            }

            if (match)
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
    }
}
