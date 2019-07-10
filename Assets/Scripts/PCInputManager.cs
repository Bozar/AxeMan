﻿using AxeMan.GameSystem;
using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.PlayerInput;
using UnityEngine;

namespace AxeMan.DungeonObject.PlayerInput
{
    public class PCInputManager : MonoBehaviour, IConvertInput, IInputManager
    {
        public IConvertInput[] InputComponent { get; private set; }

        public CommandTag ConvertInput()
        {
            return GameCore.AxeManCore.GetComponent<InputManager>()
                .ConvertInput(InputComponent);
        }

        private void Start()
        {
            InputComponent = new IConvertInput[]
            {
                GameCore.AxeManCore.GetComponent<MovementInput>(),
                GameCore.AxeManCore.GetComponent<SkillInput>(),
                GameCore.AxeManCore.GetComponent<WizardInput>(),
            };
        }

        private void Update()
        {
            GameCore.AxeManCore.GetComponent<InputManager>().PublishCommand(
                gameObject, ConvertInput());
        }
    }
}
