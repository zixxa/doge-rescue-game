namespace CustomEventBus.Signals
{
    public class ManaChangedSignal
    {
        public readonly int Mana;
        public ManaChangedSignal(int mana)
        {
            Mana = mana;
        }
        
    }
}
