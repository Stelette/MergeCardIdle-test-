using Implementation.Services;
using Implementation.StaticData;
using Implementation.StaticData.Enums;
using UnityEngine;

namespace Implementation.Factory
{
    public class CardFactory : ICardFactory
    {
        private readonly IStaticDataService _staticData;

        public CardFactory(IStaticDataService staticDataService)
        {
            _staticData = staticDataService;
        }

        public GameObject CreateCard(CardTypeId typeId)
        {
           CardStaticData staticData = _staticData.ForCard(typeId);
           GameObject card = Object.Instantiate(staticData.prefab);
           return card;
        }

        /*public GameObject CreateDamageCard()
        {
            CardStaticData staticData = _staticData.ForCard(CardTypeId.Damage);
            GameObject card = Object.Instantiate(staticData.prefab);
            DamageItem item = card.GetComponent<DamageItem>();
            item.Construct(player);
            return card;
        }
        
        public GameObject CreatePlayerCard()
        {
            CardStaticData staticData = _staticData.ForCard(CardTypeId.Player);
            GameObject card = Object.Instantiate(staticData.prefab);
            player = card.GetComponent<Item>();
            return card;
        }*/
    }
    
}