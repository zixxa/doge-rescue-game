namespace CustomEventBus.Signals
{
    public class GetLevelDataSignal
    {
    public LevelData LevelData;
    
        public GetLevelDataSignal(LevelData levelData)
        {
            LevelData = levelData;
        }
    }
}