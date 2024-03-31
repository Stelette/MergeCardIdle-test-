using Core;
using Core.StateMachine;
using Zenject;

namespace Implementation.Common
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(DiContainer diContainer,ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(diContainer,new SceneLoader(coroutineRunner));
        }
    }
}
