using UnityEngine.SceneManagement;
using CustomEventBus;
using CustomEventBus.Signals;
using UI;

public class GameController: IService, IDisposable{
    private bool gameOver;
    private EventBus _eventBus;
    private PauseController _pauseController;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _pauseController = ServiceLocator.Current.Get<PauseController>();
        _eventBus.Subscribe<GameOverSignal>(GameOver);
        _eventBus.Subscribe<SetLevelSignal>(StartGame);
        _eventBus.Subscribe<GameWinSignal>(GameWin);
    }
    
    private void StartGame(SetLevelSignal signal)
    {
        _eventBus.Invoke(new GameStartSignal());
    }
    
    private void GameOver(GameOverSignal signal)
    {
       SceneManager.LoadScene(ConstantValues.MENU_SCENE_NAME); 
    }
    
    private void GameWin(GameWinSignal signal)
    {
        _pauseController.SetPaused(true);
        DialogManager.ShowDialog<WinDialog>();
    }
    
    public void Dispose()
    {
        _eventBus.Unsubscribe<GameOverSignal>(GameOver);
        _eventBus.Unsubscribe<SetLevelSignal>(StartGame);
        _eventBus.Unsubscribe<GameWinSignal>(GameWin);
    }
}
