using AxeMan.DungeonObject;
using AxeMan.DungeonObject.ActorSkill;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using AxeMan.GameSystem.SchedulingSystem;
using AxeMan.GameSystem.SearchGameObject;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AxeMan.GameSystem
{
    public class Wizard : MonoBehaviour
    {
        private void AddStatus()
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            ActorStatus actorStatus = pc.GetComponent<ActorStatus>();

            actorStatus.AddStatus(SkillComponentTag.AirFlaw, new EffectData(2, 5));
        }

        private void ListenPCInput(PlayerCommandingEventArgs e)
        {
            if (e.SubTag != SubTag.PC)
            {
                return;
            }

            switch (e.Command)
            {
                case CommandTag.Reload:
                    SceneManager.LoadSceneAsync(0);
                    break;

                case CommandTag.PrintSchedule:
                    GetComponent<Schedule>().Print();
                    break;

                case CommandTag.ChangeHP:
                    TestHP();
                    break;

                case CommandTag.PrintSkill:
                    //PrintSkill();
                    AddStatus();
                    break;

                default:
                    break;
            }
        }

        private void PrintSkill()
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];
            PCSkillManager skillManager = pc.GetComponent<PCSkillManager>();
            var effectDict = skillManager.GetSkillEffect(SkillNameTag.E);
            string compName;
            string effect;

            foreach (var comp in effectDict.Keys)
            {
                compName = GetComponent<ConvertSkillMetaInfo>()
                    .GetSkillComponentName(comp);
                effect = GetComponent<ConvertSkillMetaInfo>()
                    .GetSkillEffectName(comp, effectDict[comp]);
                Debug.Log(compName + ": " + effect);
            }
        }

        private void Start()
        {
            GetComponent<InputManager>().PlayerCommanding
                += Wizard_PlayerCommanding;
        }

        private void TestHP()
        {
            GameObject pc = GetComponent<SearchObject>().Search(SubTag.PC)[0];

            pc.GetComponent<HP>().Subtract(5);
            pc.GetComponent<HP>().Add(2);
        }

        private void Wizard_PlayerCommanding(object sender,
            PlayerCommandingEventArgs e)
        {
            ListenPCInput(e);
        }
    }
}
