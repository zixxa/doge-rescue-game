namespace CustomEventBus.Signals
{
    public class AddManaSignal
    {
        public readonly int Value;
        public AddManaSignal(int value)
        {
            Value = value;
        }
    }
}