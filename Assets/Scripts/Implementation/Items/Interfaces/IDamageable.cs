namespace Implementation.Items.Interfaces
{
    public interface IDamageable
    {
        public int Count { get; set; }
        public int Defense { get; set; }
        public void Die();

        public bool IsDied();
        public void TakeDamage(int damage);
        public void RestoreCount(int restore);
    }
}