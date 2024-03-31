using Implementation.StaticData;
using Implementation.StaticData.Enums;
using UnityEngine;

namespace Implementation.Factory
{
    public interface ICardFactory
    {
        GameObject CreateCard(CardTypeId typeId);
    }
}