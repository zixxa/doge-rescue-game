namespace CustomEventBus.Signals
{
    public class GetPlayerSignal
    {
        public Player Player;
        public GetPlayerSignal(Player player)
        {
            Player = player;
        }
    }
}