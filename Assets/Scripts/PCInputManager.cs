using AxeMan.GameSystem;
using AxeMan.GameSystem.SchedulingSystem;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class PCInputManager : MonoBehaviour, IInputManager
    {
        private IInputManager[] input;

        public CommandTag ConvertInput()
        {
            CommandTag command;

            foreach (IInputManager i in input)
            {
                command = i.ConvertInput();
                if (command != CommandTag.INVALID)
                {
                    return command;
                }
            }
            return CommandTag.INVALID;
        }

        private void PCInputManager_EndingTurn(object sender,
            EndingTurnEventArgs e)
        {
            if (gameObject != e.Data)
            {
                return;
            }

            int[] pos = GameCore.AxeManCore.GetComponent<ConvertCoordinate>()
                .Convert(transform.position);
            Debug.Log("End: " + GetComponent<MetaInfo>().STag + ", "
                + pos[0] + ", " + pos[1]);
        }

        private void PCInputManager_StartingTurn(object sender,
            StartingTurnEventArgs e)
        {
            if (gameObject != e.Data)
            {
                return;
            }

            int[] pos = GameCore.AxeManCore.GetComponent<ConvertCoordinate>()
                .Convert(transform.position);
            Debug.Log("Start: " + GetComponent<MetaInfo>().STag + ", "
                + pos[0] + ", " + pos[1]);
        }

        private void Start()
        {
            input = new IInputManager[]
            {
                GetComponent<WizardInput>(),
                GetComponent<MovementInput>(),
            };

            GameCore.AxeManCore.GetComponent<TurnManager>().StartingTurn
                += PCInputManager_StartingTurn;
            GameCore.AxeManCore.GetComponent<TurnManager>().EndingTurn
                += PCInputManager_EndingTurn;
        }

        private void Update()
        {
            GameCore.AxeManCore.GetComponent<InputManager>().PublishCommand(
                gameObject, ConvertInput());
        }
    }
}
