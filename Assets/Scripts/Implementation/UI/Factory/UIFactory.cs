using Implementation.Infrastructure.AssetManagement;
using Implementation.Services;
using Implementation.UI.Enum;
using Implementation.UI.Windows;
using UnityEngine;
using Zenject;

namespace Implementation.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAsset _assets;
        private readonly IStaticDataService _staticData;

        private Transform _uiRoot;
        private readonly DiContainer _diContainer;

        public UIFactory(DiContainer diContainer, IAsset assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
            _diContainer = diContainer;
        }

        public WindowBase CreatePopup(WindowsId windowsId, params object[] optionalParams)
        {
            var config = _staticData.ForWindow(windowsId);
            if (config == null)
            {
                Debug.LogError($"Window Service show popup: {windowsId} no find in configs!!");
                return null;
            }

            if(_uiRoot == null)
                CreateUIRoot();
            
            WindowBase window = _diContainer.InstantiatePrefab(config.Prefab, _uiRoot).GetComponent<WindowBase>();
            window.Initialize(optionalParams);
            return window;
        }

       /*public WindowBase CreateLoadingPopup(params object[] optionalParams)
        {
            var config = _staticData.ForWindow(WindowsId.Loading);
            if(_uiRoot == null)
                CreateUIRoot();
            
            WindowBase window = _diContainer.InstantiatePrefab(config.Prefab, _uiRoot).GetComponent<WindowBase>();
            window.Initialize(optionalParams);
            return window;
        }

        public WindowBase CreateBuyDronePopup(params object[] optionalParams)
        {
            var config = _staticData.ForWindow(WindowsId.BuyDrone);
            if(_uiRoot == null)
                CreateUIRoot();
            
            WindowBase window = _diContainer.InstantiatePrefab(config.Prefab, _uiRoot).GetComponent<WindowBase>();
            window.Initialize(optionalParams);
            return window;
        }*/

        public GameObject Instantiate(GameObject prefab)
        {
            if (_uiRoot == null)
                CreateUIRoot();

            return _diContainer.InstantiatePrefab(prefab, _uiRoot);
        }

        public GameObject Instantiate(GameObject prefab, Transform at)
        {
            return _diContainer.InstantiatePrefab(prefab, at);
        }

        private void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate("UI/UIRoot").transform;
    }
}