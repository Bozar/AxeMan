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
                    match = MatchPosition(e.Position);
                    break;

                case SearchEventTag.MainTag:
                    break;

                case SearchEventTag.SubTag:
                    break;

                default:
                    break;
            }

            if (match)
            {
                e.Data.Push(gameObject);
            }
        }

        private bool MatchPosition(int[] position)
        {
            int[] localPosition = GetPosition();

            return (localPosition[0] == position[0])
                && (localPosition[1] == position[1]);
        }
    }
}
