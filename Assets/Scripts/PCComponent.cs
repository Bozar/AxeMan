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

            e.Data.AddComponent<ApplySkillFlawEffect>();

            e.Data.AddComponent<PCAttackTarget>();
            e.Data.AddComponent<PCBuffSelf>();
            e.Data.AddComponent<PCCheckTerrain>();
            e.Data.AddComponent<PCCurseTarget>();

            e.Data.AddComponent<PCInputManager>().enabled = false;
            e.Data.AddComponent<PCInputSwitcher>();

            e.Data.AddComponent<PCMove>();
            e.Data.AddComponent<PCSkillManager>();
            e.Data.AddComponent<PCStartEndTurn>();
            e.Data.AddComponent<PCTeleportSelf>();
            e.Data.AddComponent<PCUseSKill>();

            e.Data.AddComponent<SkillCooldown>();
            e.Data.AddComponent<SkillDamage>();
            e.Data.AddComponent<SkillEffect>();
            e.Data.AddComponent<SkillMetaInfo>();

            e.Data.AddComponent<SkillRange>();
            e.Data.AddComponent<SkillSlot>();
        }

        private void Start()
        {
            GetComponent<CreateObject>().AddingComponent
                += PCComponent_AddingComponent;
        }
    }
}
