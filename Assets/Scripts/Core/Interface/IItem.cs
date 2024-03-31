using UnityEngine;

namespace Core.Interface
{
    public interface IItem
    {
        Transform Transform { get; }
        void Show();
        void Hide();
        void SetWorldPosition(Vector3 position);
        Vector3 GetWorldPosition();
    }
}