using System;
using UnityEngine;

namespace Game
{
    public class GameInput : MonoBehaviour
    {
        private InputActions _inputActions;

        public Action OnInvertDirection;

        private void Awake()
        {
            _inputActions = new InputActions();
            _inputActions.Player.Enable();

            _inputActions.Player.InvertDirection.performed += InvertDirection_performed;
        }

        private void InvertDirection_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnInvertDirection?.Invoke();
        }
    }
}
