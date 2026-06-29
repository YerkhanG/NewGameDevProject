namespace model
{
    public interface IDamageable
    {
        public void TakeDamage(int damage);
        bool IsAlive { get; }
    }
}