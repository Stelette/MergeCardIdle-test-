using Implementation.Items.View;
using UnityEngine;

namespace Implementation.Items.Adapters
{
    public class ItemAdapterBase : MonoBehaviour
    {
        protected Item item => _item;
        
        protected CardView cardView => _cardView;

        [SerializeField]
        private CardView _cardView;

        [SerializeField]
        private Item _item;

        public virtual void Awake()
        {
            _item.OnCountChange += UpdateCountUI;
        }

        public virtual void Start()
        {
            UpdateCountUI();
        }

        public virtual void OnDestroy()
        {
            if(_item != null)
                _item.OnCountChange -= UpdateCountUI;
        }

        protected virtual void UpdateCountUI()
        {
            _cardView.SetCountText(_item.Count.ToString());
        }
    }
}