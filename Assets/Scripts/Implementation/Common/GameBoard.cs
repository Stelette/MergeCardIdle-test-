using System;
using System.Collections.Generic;
using Core.Enums;
using Core.Interface;
using Core.Models;
using Core.Structs;
using Cysharp.Threading.Tasks;
using Implementation.Factory;
using Implementation.Input.Interface;
using Implementation.Input.Swipe;
using Implementation.Items;
using Implementation.Items.FillStrategy;
using Implementation.Items.Jobs;
using Implementation.Providers;
using Implementation.StaticData;
using Implementation.UI.Enum;
using Implementation.UI.Services;
using UnityEngine;
using Zenject;

namespace Implementation.Common
{
    public class GameBoard : MonoBehaviour, IGameBoard
    {
        public event Action OnMoveFinish;
        public int RowCount => _rowCount;
        public int ColumnCount => _columnCount;

        public GridSlot this[int rowIndex, int columnIndex] => _gridSlots[rowIndex, columnIndex];

        [SerializeField] private int _rowCount = 9;
        [SerializeField] private int _columnCount = 9;

        [SerializeField] private Transform _board;
        [SerializeField] private float _tileOffset = 9;

        [Space] [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private Vector2 _tileSize;
        [SerializeField] private SwipeDetection _swipeDetection;

        private GridSlot[,] _gridSlots;
        private Vector3 _originPosition;
        private ICardProvider _cardProvider;
        private IWindowService _windowService;
        private IJobsExecutor _jobsExecutor;
        private bool _isFilled = false;

        private GridPosition initPlayerPosition;
        private Item playerCard;
        private GridPosition playerPosition;
        private IBoardFillStrategy _fillStrategy;

        private bool _lockInput = false;

        [Inject]
        private void Construct(ICardProvider cardProvider, IJobsExecutor jobsExecutor,IWindowService windowService)
        {
            _cardProvider = cardProvider;
            _jobsExecutor = jobsExecutor;
            _windowService = windowService;
        }

        private void Start()
        {
            Init();
            _swipeDetection.OnSwipe += DetectMove;
        }

        private async void Init()
        {
            initPlayerPosition =
                new GridPosition(Mathf.FloorToInt(RowCount / 2.0f), Mathf.FloorToInt(ColumnCount / 2.0f));
            
            _gridSlots = new GridSlot[_rowCount, _columnCount];
            _originPosition = GetOriginPosition(_rowCount, _columnCount);
            
            CreatePlayer();
            await CreateGridSlots();
            _fillStrategy = new ItemsScaleFillStrategy(_cardProvider, this);
            await FillAsync(_fillStrategy);
        }

        private async void DetectMove(SwipeDirection direction)
        {
            if (_lockInput)
                return;
            _lockInput = true;
            await MovePlayer(direction);
        }

        private async UniTask MovePlayer(SwipeDirection direction)
        {
            GridPosition nextPosition = playerPosition;
            switch (direction)
            {
                case SwipeDirection.Up:
                    nextPosition += GridPosition.Up;
                    ;
                    break;
                case SwipeDirection.Down:
                    nextPosition += GridPosition.Down;
                    ;
                    break;
                case SwipeDirection.Left:
                    nextPosition += GridPosition.Left;
                    ;
                    break;
                case SwipeDirection.Right:
                    nextPosition += GridPosition.Right;
                    ;
                    break;
            }

            if (IsPositionOnBoard(nextPosition))
            {
                GridPosition lastPosition = playerPosition;
                GridSlot destinationSlot = GetSlot(nextPosition);
                Item directionCard = destinationSlot.Item.Transform.GetComponent<Item>();

                //await move card player to destination!
                await _jobsExecutor.ExecuteJobsAsync(new[]
                    { new MoveJob(playerCard, GetWorldPosition(destinationSlot.GridPosition)) });

                directionCard.Use();
                if (playerCard.IsDied())
                {
                    Debug.Log("GAME OVER");
                    _windowService.Open(WindowsId.LoseGame);
                    _swipeDetection.OnSwipe -= DetectMove;
                }
                else
                {
                    GetSlot(lastPosition).Clear();
                    destinationSlot.SetItem(playerCard);
                    playerPosition = destinationSlot.GridPosition;
                    await FillAsync(_fillStrategy);
                }
                OnMoveFinish?.Invoke();
            }
            _lockInput = false;
        }


        public GridSlot GetSlot(GridPosition gridPosition) =>
            _gridSlots[gridPosition.RowIndex, gridPosition.ColumnIndex];

        public Vector3 GetWorldPosition(int rowIndex, int columnIndex)
        {
            Vector3 worldPosition = new Vector3(columnIndex * _tileSize.x, -rowIndex * _tileSize.y) + _originPosition;
            Vector3 offset = new Vector3(columnIndex, -rowIndex) * _tileOffset;
            return worldPosition + offset;
        }

        public Vector3 GetWorldPosition(GridPosition gridPosition)
            => GetWorldPosition(gridPosition.RowIndex, gridPosition.ColumnIndex);

        private async UniTask FillAsync(IBoardFillStrategy fillStrategy)
        {
            await _jobsExecutor.ExecuteJobsAsync(fillStrategy.GetFillJobs());
            _isFilled = true;
        }

        private async UniTask CreateGridSlots()
        {
            var itemsToShow = new List<IItem>();
            for (int rowIndex = 0; rowIndex < _rowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _columnCount; columnIndex++)
                {
                    Item item = CreateGridSlot(rowIndex, columnIndex);
                    itemsToShow.Add(item);
                }
            }

            await _jobsExecutor.ExecuteJobsAsync(new[] { new ItemShowScaleJob(itemsToShow) });
        }

        private void CreatePlayer()
        {
            Item item = _cardProvider.GetPlayerCard();
            item.Construct(10,0);
            playerCard = item;
            playerPosition = initPlayerPosition;
            GridSlot gridSlot = new GridSlot(GridSlotState.Occupied, initPlayerPosition);
            item.SetWorldPosition(GetWorldPosition(initPlayerPosition));
            gridSlot.SetItem(item);

            _gridSlots[initPlayerPosition.RowIndex, initPlayerPosition.ColumnIndex] = gridSlot;
        }
        
        private Item CreateGridSlot(int rowIndex, int columnIndex)
        {
            GridPosition gridPosition = new GridPosition(rowIndex, columnIndex);
            Item item = null;
            if (gridPosition == initPlayerPosition)
            {
                return playerCard;
            }

            item = _cardProvider.GetRandomCard();

            GridSlot gridSlot = new GridSlot(GridSlotState.Occupied, gridPosition);

            item.SetWorldPosition(GetWorldPosition(gridPosition));
            gridSlot.SetItem(item);

            _gridSlots[rowIndex, columnIndex] = gridSlot;
            return item;
        }

        /*private GridPosition GetGridPositionByPointer(Vector3 _gridPosition)
         {
             var rowIndex = (_gridPosition - _originPosition).y / _tileSize.y;
             var columnIndex = (_gridPosition - _originPosition).x / _tileSize.x;
 
             return new GridPosition(Convert.ToInt32(-rowIndex), Convert.ToInt32(columnIndex));
         }*/

        private bool IsPositionOnBoard(GridPosition gridPosition)
        {
            return gridPosition.RowIndex >= 0 &&
                   gridPosition.RowIndex < _rowCount &&
                   gridPosition.ColumnIndex >= 0 &&
                   gridPosition.ColumnIndex < _columnCount;
        }

        private Vector3 GetOriginPosition(int rowCount, int columnCount)
        {
            float offsetY = Mathf.Floor(rowCount / 2.0f) * _tileSize.y + (Mathf.Floor(rowCount / 2.0f) * _tileOffset);
            float offsetX = Mathf.Floor(columnCount / 2.0f) * _tileSize.x +
                            (Mathf.Floor(columnCount / 2.0f) * _tileOffset);
            return new Vector3(-offsetX, offsetY);
        }

        private void OnDestroy()
        {
            if (_swipeDetection != null)
                _swipeDetection.OnSwipe -= DetectMove;
        }
    }
}