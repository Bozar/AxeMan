using AxeMan.DungeonObject;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class ActorComponent : MonoBehaviour
    {
        private void ActorComponent_AddingComponent(object sender,
            AddingComponentEventArgs e)
        {
            if (e.Data.GetComponent<MetaInfo>().MTag == MainTag.Actor)
            {
                e.Data.AddComponent<HP>();
            }
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += ActorComponent_AddingComponent;
        }
    }
}
