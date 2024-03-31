using Implementation.UI.Enum;
using Implementation.UI.Windows;
using UnityEngine;

namespace Implementation.UI.Factory
{
    public interface IUIFactory
    {
        WindowBase CreatePopup(WindowsId windowsId, params object[] optionalParams);

        public GameObject Instantiate(GameObject prefab);

        public GameObject Instantiate(GameObject prefab, Transform at);
    }
}