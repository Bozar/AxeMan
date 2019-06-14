using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface ISearchObject
    {
        GameObject[] Search(int x, int y);

        GameObject[] Search(MainTag mTag);

        GameObject[] Search(SubTag sTag);
    }

    public class SearchObject : MonoBehaviour, ISearchObject
    {
        public GameObject[] Search(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public GameObject[] Search(MainTag mTag)
        {
            throw new System.NotImplementedException();
        }

        public GameObject[] Search(SubTag sTag)
        {
            throw new System.NotImplementedException();
        }
    }
}
