using UnityEngine;

namespace AxeMan.GameSystem
{
    public class Wizard : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(FindObjectOfType<GameCore>().Hello);
        }
    }
}
