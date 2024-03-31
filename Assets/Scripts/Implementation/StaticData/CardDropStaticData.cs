using UnityEngine;
using Utility.LootDrops.LootDropsItem;

namespace Implementation.StaticData
{
    [CreateAssetMenu(fileName = "CardDropData",menuName = "StaticData/Card Drop")]
    public class CardDropStaticData: ScriptableObject
    {
        public LootDropTableItem LootDropTableItem;
        
        void OnValidate(){

            // Validate table and notify the programmer / designer if something went wrong.
            LootDropTableItem.ValidateTable();
        }
    }
}