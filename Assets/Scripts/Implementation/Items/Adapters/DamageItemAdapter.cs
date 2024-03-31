using UnityEngine;
using UnityEngine.Serialization;

namespace Implementation.Items.Adapters
{
    public class DamageItemAdapter : ItemAdapterBase
    {
        [SerializeField]
        private Sprite[] sprites;

        public override void Start()
        {
            base.Start();
            cardView.SetSprite(GetRandomSprite());
        }

        private Sprite GetRandomSprite()
        {
            return sprites[Random.Range(0, sprites.Length)];
        }
    }
}