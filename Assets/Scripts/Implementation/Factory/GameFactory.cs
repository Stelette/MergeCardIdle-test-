using Implementation.AssetsManagment;

namespace Implementation.Factory
{
    public class GameFactory
    {
        private readonly IAssetsProvider _assets;

        public GameFactory(IAssetsProvider assetsProvider)
        {
            _assets = assetsProvider;
        }
        
        public void CreateHeroCard()
        {
            _assets.Instantiate(AssetPath.LevelFinishHUD);
        }
    }
}