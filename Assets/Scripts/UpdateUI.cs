using UnityEngine;
using UnityEngine.UI;

// NOTE: This is a test to update UI content by publishing events. Delete or
// change this component later. This component is attached to
// `Canvas_World/Modeline`.
namespace AxeMan.GameSystem
{
    public class UpdateUI : MonoBehaviour
    {
        private void Start()
        {
            GameCore.AxeManCore.GetComponent<Wizard>().UIText += UpdateUI_UIText;
            Debug.Log(tag);
        }

        private void UpdateUI_UIText(object sender, UpdateUIEventArgs e)
        {
            GetComponent<Text>().text = e.UIText;
        }
    }
}
