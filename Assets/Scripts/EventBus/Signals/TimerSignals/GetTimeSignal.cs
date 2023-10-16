namespace CustomEventBus.Signals
{
    public class GetTimeScaleSignal
    {
        public readonly int TimeScale;

        public GetTimeScaleSignal(int time)
        {
            TimeScale = time;
        }
    }
}