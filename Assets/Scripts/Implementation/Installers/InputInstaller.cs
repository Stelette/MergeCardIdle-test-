using Implementation.Input;
using Implementation.Input.Interface;
using Zenject;

namespace Implementation.Installers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        { 
            InputManager inputManager = new InputManager();
            inputManager.Init();
            Container.Bind<IInputManager>().FromInstance(inputManager).AsSingle();
        }
    }
}