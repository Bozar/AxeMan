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
            Debug.Log(GetComponentInParent<Canvas>().name);
        }

        private void UpdateUI_UIText(object sender, UpdateUIEventArgs e)
        {
            if ((tag == e.UITag)
                && (e.UIData.TryGetValue(name, out string value)))
            {
                GetComponent<Text>().text = value;
            }
        }
    }
}
