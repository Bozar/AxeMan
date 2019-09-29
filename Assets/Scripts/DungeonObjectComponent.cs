using AxeMan.DungeonObject;
using AxeMan.DungeonObject.SearchGameObject;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class DungeonObjectComponent : MonoBehaviour
    {
        private void DungeonObjectComponent_AddingComponent(object sender,
            AddingComponentEventArgs e)
        {
            if (e.Data.GetComponent<MetaInfo>().MainTag == MainTag.INVALID)
            {
                return;
            }
            e.Data.AddComponent<LocalManager>();
            e.Data.AddComponent<SubscribeSearch>();
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += DungeonObjectComponent_AddingComponent;
        }
    }
}
