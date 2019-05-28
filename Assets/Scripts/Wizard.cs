using UnityEngine;

namespace AxeMan.GameSystem
{
    public class Wizard : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(GameCore.AxeManCore.GetComponent<GameCore>().Hello);
            //Debug.Log(GetComponent<GameCore>().Hello);
            //Debug.Log(FindObjectOfType<GameCore>().Hello);
        }
    }
}
