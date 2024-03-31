using Implementation.Items.Interfaces;

namespace Implementation.Items
{
    public class DamageItem : Item
    {
        private IDamageable _player;
        
        public void Construct(IDamageable playerDamageable)
        {
            _player = playerDamageable;
        }

        public override void Use()
        {
            _player.TakeDamage(Count);

            if (!_player.IsDied())
            {
                //self-destroy
                Die();
            }
        }
    }
}