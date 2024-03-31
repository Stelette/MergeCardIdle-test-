using Implementation.UI.Factory;
using Implementation.UI.Services;
using Zenject;

namespace Implementation.Installers
{
    public class WindowServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            IWindowService windowService = new WindowService(Container.Resolve<IUIFactory>());
            Container.Bind<IWindowService>().FromInstance(windowService).AsSingle();
        }
    }
}