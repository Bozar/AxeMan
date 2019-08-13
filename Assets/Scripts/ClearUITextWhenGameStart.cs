using UnityEngine;
using UnityEngine.UI;

namespace AxeMan.GameSystem.UserInterface
{
    public class ClearUITextWhenGameStart : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Text>().text = "";
        }
    }
}
