using AxeMan.DungeonObject;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class NPCComponent : MonoBehaviour
    {
        private void NPCComponent_AddingComponent(object sender,
            AddingComponentEventArgs e)
        {
            if (e.Data.GetComponent<MetaInfo>()?.STag != SubTag.Dummy)
            {
                return;
            }
            e.Data.AddComponent<DummyAI>();
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += NPCComponent_AddingComponent;
        }
    }
}
