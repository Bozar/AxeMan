using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.GameMode;
using System.Collections.Generic;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class PCUseSKill : MonoBehaviour
    {
        private Dictionary<CommandTag, ActionTag> command2action;

        private void Awake()
        {
            command2action = new Dictionary<CommandTag, ActionTag>()
            {
                { CommandTag.SkillQ, ActionTag.UseSkillQ },
                { CommandTag.SkillW, ActionTag.UseSkillW },
                { CommandTag.SkillE, ActionTag.UseSkillE },
                { CommandTag.SkillR, ActionTag.UseSkillR },
            };
        }

        private void PCUseSKill_VerifiedSkill(object sender,
            VerifiedSkillEventArgs e)
        {
            MainTag mainTag;
            SubTag subTag;
            int id;

            if (command2action.TryGetValue(e.UseSkill, out ActionTag action))
            {
                mainTag = GetComponent<MetaInfo>().MainTag;
                subTag = GetComponent<MetaInfo>().SubTag;
                id = GetComponent<MetaInfo>().ObjectID;

                GetComponent<LocalManager>().TakingAction(
                    new TakingActionEventArgs(action, mainTag, subTag, id));
                GetComponent<LocalManager>().TakenAction(
                    new TakenActionEventArgs(action, mainTag, subTag, id));
            }
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<AimMode>().VerifiedSkill
                += PCUseSKill_VerifiedSkill;
        }
    }
}
