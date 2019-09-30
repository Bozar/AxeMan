using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.GameEvent;
using AxeMan.GameSystem.SchedulingSystem;
using AxeMan.GameSystem.SearchGameObject;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AxeMan.DungeonObject
{
    public class ActiveTrap : MonoBehaviour
    {
        private GameObject[] traps;
        private Stack<SubTag> trapTags;
        private ActionTag[] validActions;

        private void ActiveTrap_EndingTurn(object sender,
            StartOrEndTurnEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID))
            {
                return;
            }

            while (trapTags.Count > 0)
            {
                BuildingEffect building
                    = GameCore.AxeManCore.GetComponent<BuildingEffect>();
                MainTag mainTag = MainTag.Trap;
                SubTag subTag = trapTags.Pop();
                SkillComponentTag skill = building.GetEffect(mainTag, subTag);
                int data = building.GetPowerDuration(mainTag, subTag);

                GetComponent<ActorStatus>().AddStatus(skill,
                    new EffectData(data, data));
            }
        }

        private void ActiveTrap_TakenAction(object sender,
            PublishActionEventArgs e)
        {
            if (!GetComponent<LocalManager>().MatchID(e.ObjectID)
                || !validActions.Contains(e.Action))
            {
                return;
            }

            int[] pos = GetComponent<MetaInfo>().Position;
            GameCore.AxeManCore.GetComponent<SearchObject>().Search(
                pos[0], pos[1], MainTag.Trap, out traps);

            foreach (GameObject go in traps)
            {
                trapTags.Push(go.GetComponent<MetaInfo>().SubTag);
                go.GetComponent<LocalManager>().Remove();
            }
        }

        private void Awake()
        {
            trapTags = new Stack<SubTag>();

            validActions = new ActionTag[]
            {
                ActionTag.Move,
                ActionTag.UseSkillQ,
                ActionTag.UseSkillW,
                ActionTag.UseSkillE,
                ActionTag.UseSkillR,
                ActionTag.NPCFindPath,
            };
        }

        private void Start()
        {
            GameCore.AxeManCore.GetComponent<PublishAction>().TakenAction
                += ActiveTrap_TakenAction;
            GameCore.AxeManCore.GetComponent<TurnManager>().EndingTurn
                += ActiveTrap_EndingTurn;
        }
    }
}
