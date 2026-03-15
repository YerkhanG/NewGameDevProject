namespace model
{
    public interface IDamageable
    {
        public void TakeDamage(float damage);
        bool IsAlive { get; }
    }
}