using Implementation.UI.Enum;
using Implementation.UI.Factory;
using Implementation.UI.Windows;

namespace Implementation.UI.Services
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        private WindowBase _currentWindow;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowsId windowsId, params object[] optionalParams)
        {
            switch (windowsId)
            {
                /*case WindowsId.Loading:
                    _currentWindow = _uiFactory.CreateLoadingPopup(optionalParams);
                    break;
                    */
                default:
                    _currentWindow = _uiFactory.CreatePopup(windowsId,optionalParams);
                    break;
            }
        }

        public void CloseCurrentWindow()
        {
            if (_currentWindow != null)
                UnityEngine.Object.Destroy(_currentWindow.gameObject);
        }
    }
}