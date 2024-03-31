using UnityEngine;

namespace Utility.LootDrops.LootDropsGameObject
{
    /// <summary>
    /// When we're inheriting we have to insert GameObject as a type to GenericLootDropItem
    /// </summary>
    [System.Serializable]
    public class GenericLootDropItemGameObject : GenericLootDropItem<GameObject> {}
}