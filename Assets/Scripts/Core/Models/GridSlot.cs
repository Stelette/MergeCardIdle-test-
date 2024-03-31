using Core.Enums;
using Core.Interface;
using Core.Structs;

namespace Core.Models
{
    public class GridSlot
    {
        public IItem Item { get; private set; }
        public GridPosition GridPosition { get; }
        public GridSlotState State { get; private set; }
        
        public GridSlot(GridSlotState state, GridPosition gridPosition)
        {
            State = state;
            GridPosition = gridPosition;
        }
        
        public void SetItem(IItem item)
        {
            Item = item;
            State = GridSlotState.Occupied;
        }

        public void Unlock()
        {
            State = GridSlotState.Free;
        }
        
        public void Clear()
        {
            Item = null;
            State = GridSlotState.Free;
        }
    }
}