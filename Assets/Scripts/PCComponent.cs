using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.DungeonObject.PlayerInput;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem.ObjectFactory
{
    public class PCComponent : MonoBehaviour
    {
        private void PCComponent_AddingComponent(object sender,
            AddingComponentEventArgs e)
        {
            if (e.Data.GetComponent<MetaInfo>()?.SubTag != SubTag.PC)
            {
                return;
            }
            e.Data.AddComponent<PCCheckTerrain>();
            e.Data.AddComponent<PCInputManager>().enabled = false;
            e.Data.AddComponent<PCInputSwitcher>();

            e.Data.AddComponent<PCMove>();
            e.Data.AddComponent<PCSkillManager>();
            e.Data.AddComponent<PCStartEndTurn>();
            e.Data.AddComponent<PCUseSKill>();

            e.Data.AddComponent<SkillMetaInfo>();
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += PCComponent_AddingComponent;
        }
    }
}
