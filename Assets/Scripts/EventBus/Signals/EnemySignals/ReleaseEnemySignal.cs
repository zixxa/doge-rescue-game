namespace CustomEventBus.Signals
{
    public class ReleaseEnemySignal
    {
        public Enemy Enemy;

        public ReleaseEnemySignal(Enemy enemy)
        {
            Enemy = enemy;
        }
    }
}