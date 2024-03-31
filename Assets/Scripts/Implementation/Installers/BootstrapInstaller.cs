using Core;
using Core.Interface;
using Implementation.Factory;
using Implementation.Services;
using Zenject;

namespace Implementation.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Container.Bind<DiContainer>().FromInstance(Container).AsSingle();
            IStaticDataService staticDataService = RegisterStaticData();
            ICardFactory cardFactory = new CardFactory(staticDataService);
            Container.Bind<ICardFactory>().FromInstance(cardFactory).AsSingle();
            Container.Bind<IJobsExecutor>().FromInstance(new JobsExecutor()).AsSingle();
            
        }

        private IStaticDataService RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.Initialize();
            staticDataService.LoadCards();
            Container.Bind<IStaticDataService>().FromInstance(staticDataService).AsSingle();
            return staticDataService;
        }
    }
}
