using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using Levels;

public class LevelController : IService, IDisposable
{
    private int _currentLevelId;
    private LevelDataManager _LevelDataManager;
    private EventBus _eventBus;
    private Level _level;
    private int _levelId;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<LevelPassedSignal>(LevelPassed);
        _eventBus.Subscribe<NextLevelSignal>(NextLevel);
        _eventBus.Subscribe<RestartLevelSignal>(RestartLevel);

        _LevelDataManager = ServiceLocator.Current.Get<LevelDataManager>();

        if (PlayerPrefs.GetInt(ConstantValues.CURRENT_LEVEL_NAME)>ConstantValues.LEVEL_COUNT)
            PlayerPrefs.SetInt(ConstantValues.CURRENT_LEVEL_NAME, 0);
        _currentLevelId = PlayerPrefs.GetInt(ConstantValues.CURRENT_LEVEL_NAME, 0);
        OnInit();
    }

   private void OnInit()
   {
       LoadLevel(_currentLevelId);
   } 
    private void LoadLevel(int levelId){
        var _level = _LevelDataManager.ShowLevel<Level>(levelId);
        if (_level == null)
        {
            Debug.LogErrorFormat("Can't find level with id {0}", _currentLevelId);
            return;
        }
        _eventBus.Invoke(new SetLevelSignal());
    }


    private void NextLevel(NextLevelSignal signal)
    {
        if (_currentLevelId == _LevelDataManager.LevelsCount)
            _eventBus.Invoke(new GameWinSignal());
        else
            LoadLevel(_currentLevelId);
    }

    private void ClearLevel()
    {
        _eventBus.Invoke(new GameClearSignal());
    }

    private void RestartLevel(RestartLevelSignal signal)
    {
        ClearLevel();
        LoadLevel(_currentLevelId);
    }
    private void LevelPassed(LevelPassedSignal signal)
    {
        ClearLevel();
        _currentLevelId++;
        PlayerPrefs.SetInt(ConstantValues.CURRENT_LEVEL_NAME, _currentLevelId);
        _eventBus.Invoke(new NextLevelSignal());
    }
    public void Dispose()
    {
        _eventBus.Unsubscribe<LevelPassedSignal>(LevelPassed);
        _eventBus.Unsubscribe<NextLevelSignal>(NextLevel);
        _eventBus.Unsubscribe<RestartLevelSignal>(RestartLevel);
    }
}