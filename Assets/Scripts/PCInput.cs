using AxeMan.GameSystem;
using UnityEngine;

namespace AxeMan.Actor.PlayerInput
{
    public interface IInputManager
    {
        CommandTag ConvertInput();
    }

    public class PCInput : MonoBehaviour, IInputManager
    {
        public CommandTag ConvertInput()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("Input j");
                Debug.Log(GetComponent<MetaInfo>().STag);
                return CommandTag.Test;
            }
            return CommandTag.INVALID;
        }

        private void Update()
        {
            ConvertInput();
        }
    }
}
