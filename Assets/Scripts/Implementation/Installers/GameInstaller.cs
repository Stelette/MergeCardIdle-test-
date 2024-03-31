using Core.Models;
using Implementation.Common;
using Implementation.Factory;
using Implementation.Providers;
using Implementation.Services;
using Zenject;

namespace Implementation.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public GameBoard gameBoard;
        public override void InstallBindings()
        { 
            Container.Bind<IGameBoard>().FromInstance(gameBoard).AsSingle();
            RegisterCardProvider(Container.Resolve<IStaticDataService>(),Container.Resolve<ICardFactory>());
        }
        
        private void RegisterCardProvider(IStaticDataService staticDataService, ICardFactory cardFactory)
        {
            ICardProvider cardProvider = new CardProvider(staticDataService, cardFactory,gameBoard);
            cardProvider.Init();
            Container.Bind<ICardProvider>().FromInstance(cardProvider);
        }
    }
}