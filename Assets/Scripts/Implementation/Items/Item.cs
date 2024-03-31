using System;
using Core.Interface;
using Implementation.Items.Interfaces;
using UnityEngine;

namespace Implementation.Items
{
    public abstract class Item : MonoBehaviour, IDamageable, IItem
    {
        public Transform Transform => transform;
        public event Action OnCountChange;
        public event Action OnDefenseChange;
        public event Action<Item> OnDie;
        
        public int Count { get; set; }
        public int Defense { get; set; }

        public void Construct(int count,int defense)
        {
            Count = count;
            Defense = defense;
            OnCountChange?.Invoke();
            OnDefenseChange?.Invoke();
        }

        public abstract void Use();

        public void Die()
        {
            OnDie?.Invoke(this);
            Destroy(gameObject);
        }

        public virtual bool IsDied() => Count <= 0;

        public virtual void TakeDamage(int damage)
        {
            if (Defense >= damage)
                Defense -= damage;
            else
            {
                damage -= Defense;
                Defense = 0;
                RemoveCount(damage);
                if(Count <= 0)
                    Die();
            }
            OnDefenseChange?.Invoke();
        }

        public virtual void RestoreCount(int restore)
        {
            Count += restore;
            OnCountChange?.Invoke();
        }

        protected void RemoveCount(int damage)
        {
            Count -= damage;
            OnCountChange?.Invoke();
        }
        
        /// <summary>
        /// IItem interface
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetWorldPosition(Vector3 position)
        {
            transform.position = position;
        }

        public Vector3 GetWorldPosition()
        {
            return transform.position;
        }
    }
}