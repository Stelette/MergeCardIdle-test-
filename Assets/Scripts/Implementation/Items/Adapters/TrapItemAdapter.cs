using UnityEngine;

namespace Implementation.Items.Adapters
{
    public class TrapItemAdapter : ItemAdapterBase
    {
        [SerializeField]
        private Sprite EnableIcon;
        
        [SerializeField]
        private Sprite DisableIcon;
        
        protected override void UpdateCountUI()
        {
            base.UpdateCountUI();
            cardView.SetSprite(item.Count == 0 ? DisableIcon : EnableIcon);
        }
    }
}