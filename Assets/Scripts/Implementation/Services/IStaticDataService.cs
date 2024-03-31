using Implementation.StaticData;
using Implementation.StaticData.Enums;
using Implementation.StaticData.Window;
using Implementation.UI.Enum;
using Utility.LootDrops.LootDropsItem;

namespace Implementation.Services
{
    public interface IStaticDataService
    {
        void Initialize();
        void LoadCards();
        CardStaticData ForCard(CardTypeId typeId);

        LootDropTableItem GetCardDropStaticData();
        
        WindowConfig ForWindow(WindowsId windowsId);
    }
}