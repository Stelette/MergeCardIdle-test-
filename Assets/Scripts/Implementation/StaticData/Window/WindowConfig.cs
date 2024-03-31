using System;
using Implementation.UI.Enum;
using Implementation.UI.Windows;

namespace Implementation.StaticData.Window
{
    [Serializable]
    public class WindowConfig
    {
        public WindowsId WindowsId;
        public WindowBase Prefab;
    }
}