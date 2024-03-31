using Implementation.StaticData.Enums;
using UnityEngine;

namespace Implementation.StaticData
{
    [CreateAssetMenu(fileName = "CardData",menuName = "StaticData/Card")]
    public class CardStaticData : ScriptableObject
    {
        public CardTypeId CardTypeId;
        public GameObject prefab;
    }
}