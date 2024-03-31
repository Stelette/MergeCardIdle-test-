using System.Collections.Generic;
using UnityEngine;

namespace Implementation.StaticData.Window
{
    [CreateAssetMenu(menuName = "Static Data/Window static data",fileName = "WindowStaticData")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> configs;
    }
}