using System.Collections.Generic;
using Core.Enums;
using Core.Interface;
using Core.Models;
using Implementation.Factory;
using Implementation.Items.Jobs;
using Implementation.Providers;
using Implementation.StaticData;

namespace Implementation.Items.FillStrategy
{
    public class ItemsScaleFillStrategy : IBoardFillStrategy
    {
        public string Name { get; }
        
        private readonly IGameBoard _gameBoard;
        private readonly ICardProvider _cardProvider;

        public ItemsScaleFillStrategy(ICardProvider cardProvider,IGameBoard gameBoard)
        {
            _cardProvider = cardProvider;
            _gameBoard = gameBoard;
        }


        public IEnumerable<IJob> GetFillJobs()
        {
            var itemsToShow = new List<IItem>();
            for (int rowIndex = 0; rowIndex < _gameBoard.RowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _gameBoard.ColumnCount; columnIndex++)
                {
                    GridSlot gridSlot = _gameBoard[rowIndex, columnIndex];
                    if (gridSlot.State != GridSlotState.Free)
                    {
                        continue;
                    }

                    IItem item = _cardProvider.GetRandomCard().GetComponent<IItem>();
                    item.SetWorldPosition(_gameBoard.GetWorldPosition(rowIndex, columnIndex));

                    gridSlot.SetItem(item);
                    itemsToShow.Add(item);
                }
            }

            return new[] { new ItemShowScaleJob(itemsToShow) };
        }
    }
}