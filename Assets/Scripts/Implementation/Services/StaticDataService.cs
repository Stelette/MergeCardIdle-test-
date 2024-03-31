using System.Collections.Generic;
using System.Linq;
using Implementation.StaticData;
using Implementation.StaticData.Enums;
using Implementation.StaticData.Window;
using Implementation.UI.Enum;
using UnityEngine;
using Utility.LootDrops.LootDropsItem;

namespace Implementation.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataWIndowsPath = "StaticData/Windows/WindowStaticData";
        private const string staticdataCards = "StaticData/Cards";
        private const string staticdataCarddropdata = "StaticData/CardDropData";
        
        private Dictionary<CardTypeId,CardStaticData> _cards;
        private Dictionary<WindowsId, WindowConfig> _windowConfigs;

        public void LoadCards()
        {
           
            _cards = Resources.LoadAll<CardStaticData>(staticdataCards)
                .ToDictionary(x => x.CardTypeId, x => x);
        }

        public CardStaticData ForCard(CardTypeId typeId) => 
            _cards.TryGetValue(typeId, out CardStaticData staticData)
                ? staticData
                : null;

        public LootDropTableItem GetCardDropStaticData()
        {
            
            CardDropStaticData cardDropTable = Resources.Load<CardDropStaticData>(staticdataCarddropdata);
            return cardDropTable.LootDropTableItem;
        }
        
        public void Initialize()
        {
            _windowConfigs = Resources.Load<WindowStaticData>(StaticDataWIndowsPath)
                .configs
                .ToDictionary(x => x.WindowsId, x => x);
        }

        public WindowConfig ForWindow(WindowsId windowsId) =>
            _windowConfigs.TryGetValue(windowsId, out WindowConfig windowConfig)
                ? windowConfig
                : null;
    }
}