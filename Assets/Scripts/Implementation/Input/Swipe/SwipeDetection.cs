using System;
using Core.Enums;
using Implementation.Input.Interface;
using UnityEngine;
using Zenject;

namespace Implementation.Input.Swipe
{
    public class SwipeDetection : MonoBehaviour
    {
        public event Action<SwipeDirection> OnSwipe; 

        [SerializeField] 
        private float minDistanceSwipe = 0.2f;

        [SerializeField] 
        private float maxSwipeTime = 1f;
        
        [SerializeField,Range(0f,1f)] 
        private float directionThreshold = 0.9f;
        
        [SerializeField] 
        private Camera _camera;
        
        private IInputManager _inputManager;
        private Vector2 _startPosition;
        private float _startTime;
        private Vector2 _endPosition;
        private float _endTime;
        
        [Inject]
        public void Construct(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void Start()
        {
            _inputManager.OnStartTouch += StartTouch;
            _inputManager.OnEndTouch += EndTouch;
            if (_camera == null)
                _camera = Camera.main;
        }
        
        private void OnDestroy()
        {
            if (_inputManager != null)
            {
                _inputManager.OnStartTouch -= StartTouch;
                _inputManager.OnEndTouch -= EndTouch;
            }
        }

        private void StartTouch(Vector2 position, float time)
        {
            _startPosition = position;
            _startTime = time;
        }
        
        private void EndTouch(Vector2 position, float time)
        {
            _endPosition = position;
            _endTime = time;
            DetectSwipe();
        }

        private void DetectSwipe()
        {
            Vector3 startPos = ScreenToWorld(_startPosition);
            Vector3 endPos = ScreenToWorld(_endPosition);
            if (IsAcceptSwipeTime() && IsAvailableDistance(startPos,endPos))
            {
                Vector3 dir = endPos - startPos;
                Vector2 dir2D = new Vector2(dir.x, dir.y).normalized;
                SwipeDirection(dir2D);
            }
        }

        private bool IsAcceptSwipeTime() 
            => (_endTime - _startTime) <= maxSwipeTime;

        private bool IsAvailableDistance(Vector2 start,Vector2 end) 
            => Vector3.Distance(start, end) >= minDistanceSwipe;

        private void SwipeDirection(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
            {
                OnSwipe?.Invoke(Core.Enums.SwipeDirection.Up);
            }
            else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
            {
                OnSwipe?.Invoke(Core.Enums.SwipeDirection.Down);
            }
            else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
            {
                OnSwipe?.Invoke(Core.Enums.SwipeDirection.Left);
            }
            else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
            {
                OnSwipe?.Invoke(Core.Enums.SwipeDirection.Right);
            }
        }

        private Vector3 ScreenToWorld(Vector3 position)
        {
            position.z = _camera.nearClipPlane;
            return _camera.ScreenToWorldPoint(position);
        }
    }
}