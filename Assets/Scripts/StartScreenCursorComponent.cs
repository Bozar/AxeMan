using AxeMan.DungeonObject;
using AxeMan.DungeonObject.PlayerInput;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class StartScreenCursorComponent : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += StartScreenCursorComponent_AddingComponent;
        }

        private void StartScreenCursorComponent_AddingComponent(object sender,
            AddingComponentEventArgs e)
        {
            if (e.Data.GetComponent<MetaInfo>().SubTag
                != SubTag.StartScreenCursor)
            {
                return;
            }
            e.Data.AddComponent<StartScreenCursorInputManager>();
            e.Data.AddComponent<StartScreenCursorInputSwitcher>();
        }
    }
}
