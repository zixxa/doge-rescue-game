namespace CustomEventBus.Signals
{
    public class RedrawHPSignal
    {
        public readonly int HP;

        public RedrawHPSignal(int hp)
        {
            HP = hp;
        }
    }
}