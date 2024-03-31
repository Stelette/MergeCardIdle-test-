using UnityEngine;

namespace Implementation.Input.Interface
{
    public interface IInputManager
    {
        public delegate void StartTouch(Vector2 position, float time);
        public event StartTouch OnStartTouch;
        
        public delegate void EndTouch(Vector2 position, float time);
        public event EndTouch OnEndTouch;
        void EnableInput();
        void DisableInput();
    }
}