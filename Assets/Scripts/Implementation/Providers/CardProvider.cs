using System;
using Core.Models;
using Implementation.Factory;
using Implementation.Items;
using Implementation.Services;
using Implementation.StaticData.Enums;
using UnityEngine;
using Utility.LootDrops.LootDropsItem;
using Random = UnityEngine.Random;

namespace Implementation.Providers
{
    public class CardProvider : ICardProvider
    {
        private LootDropTableItem _dropTable;

        private Item _playerCard;

        private const int INIT_PLAYER_COUNT = 10;
        
        private readonly IStaticDataService _staticData;
        private readonly ICardFactory _cardFactory;
        private readonly IGameBoard _gameBoard;

        public CardProvider(IStaticDataService staticData,ICardFactory cardFactory,IGameBoard gameBoard)
        {
            _staticData = staticData;
            _cardFactory = cardFactory;
            _gameBoard = gameBoard;
        }

        public void Init()
        {
            _dropTable = _staticData.GetCardDropStaticData();
        }

        public Item GetRandomCard()
        {
            CardTypeId typeId = _dropTable.PickLootDropItem().item;
            Item item = _cardFactory.CreateCard(typeId).GetComponent<Item>();
            switch (typeId)
            {
                case CardTypeId.Player:
                    break;
                case CardTypeId.Damage:
                    (item as DamageItem)?.Construct(_playerCard);
                    break;
                case CardTypeId.Health:
                    (item as HealthItem)?.Construct(_playerCard);
                    break;
                case CardTypeId.Trap:
                    (item as TrapItem)?.Construct(_playerCard,_gameBoard);
                    break;
                case CardTypeId.Empty:
                    break;
            }

            PrepareItem(item);
            return item;
        }

        private void PrepareItem(Item item) =>
            item.Construct(GetInitCount(),0);

        private int GetInitCount() =>
            Random.Range(1, Mathf.Max(_playerCard.Count - 3,4));

        public Item GetPlayerCard()
        {
            _playerCard = _cardFactory.CreateCard(CardTypeId.Player).GetComponent<Item>();
            _playerCard.Construct(INIT_PLAYER_COUNT,0);
            return _playerCard;
        }
    }
}