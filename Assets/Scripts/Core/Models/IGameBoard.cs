
using System;
using Core.Structs;
using UnityEngine;

namespace Core.Models
{
    public interface IGameBoard
    {
        event Action OnMoveFinish;

        int RowCount { get; }
        int ColumnCount { get; }
        
        GridSlot this[int rowIndex, int columnIndex] { get; }
        GridSlot GetSlot(GridPosition gridPosition);

        public Vector3 GetWorldPosition(int rowIndex, int columnIndex);
        
        public Vector3 GetWorldPosition(GridPosition gridPosition);
    }
}