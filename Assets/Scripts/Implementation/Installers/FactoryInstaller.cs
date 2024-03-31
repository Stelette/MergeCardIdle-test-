using Implementation.Infrastructure.AssetManagement;
using Implementation.Services;
using Implementation.UI.Factory;
using Zenject;

namespace Implementation.Installers
{
    public class FactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var assetService = RegisterAssetService();
            
            IUIFactory uiFactory = new UIFactory(Container, assetService, Container.Resolve<IStaticDataService>());
            Container.Bind<IUIFactory>().FromInstance(uiFactory).AsSingle();
        }
        
        private IAsset RegisterAssetService()
        {
            IAsset assetService = new AssetProvider();
            Container.Bind<IAsset>().FromInstance(assetService).AsSingle();
            return assetService;
        }
    }
}