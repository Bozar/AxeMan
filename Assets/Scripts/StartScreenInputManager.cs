﻿using UnityEngine;

namespace AxeMan.GameSystem.PlayerInput
{
    public class StartScreenInputManager : MonoBehaviour, IInputManager
    {
        private IConvertInput[] inputComponents;

        public IConvertInput[] GetInputComponent()
        {
            return inputComponents;
        }

        private void Start()
        {
            inputComponents = new IConvertInput[]
            {
                GetComponent<ConfirmCancelInput>(),

                // TODO: Delete this component later.
                GetComponent<WizardInput>(),
            };
        }
    }
}
