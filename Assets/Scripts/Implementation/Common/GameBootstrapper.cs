using Core;
using Core.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Implementation.Common
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        private DiContainer _container;


        [Inject]
        public void Init(DiContainer container)
        {
            _container = container;
        }
        
        private void Start()
        {
            _game = new Game(_container,this);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        }
    }
}