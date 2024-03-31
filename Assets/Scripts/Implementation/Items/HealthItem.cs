using Implementation.Items.Interfaces;

namespace Implementation.Items
{
    public class HealthItem : Item
    {
        private IDamageable _playerDamageable;

        public void Construct(IDamageable playerDamageable)
        {
            _playerDamageable = playerDamageable;
        }

        public override void Use()
        {
            _playerDamageable.RestoreCount(Count);
            //self-destroy
            Die();
        }
    }
}