using Implementation.Input.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Implementation.Input
{
    public class InputManager : IInputManager
    {
        #region Events

        public event IInputManager.StartTouch OnStartTouch;
        public event IInputManager.EndTouch OnEndTouch;

        #endregion


        private InputController _inputController;

        public void Init()
        {
            _inputController = new InputController();
            EnableInput();
            _inputController.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
            _inputController.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        }

        public void EnableInput()
        {
            _inputController.Enable();
        }

        public void DisableInput()
        {
            _inputController.Disable();
        }

        private void StartTouchPrimary(InputAction.CallbackContext ctx)
        {
            OnStartTouch?.Invoke(_inputController.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)ctx.startTime);
        }

        private void EndTouchPrimary(InputAction.CallbackContext ctx)
        {
            OnEndTouch?.Invoke(_inputController.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)ctx.time);
        }
    }
}