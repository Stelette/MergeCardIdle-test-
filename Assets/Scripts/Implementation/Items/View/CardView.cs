using TMPro;
using UnityEngine;

namespace Implementation.Items.View
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countT;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        
        
        public void SetCountText(string text)
        {
            countT.text = text;
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}