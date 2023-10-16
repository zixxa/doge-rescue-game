using CustomEventBus;
using UnityEngine;
using UI;
using Levels;
using System.Collections.Generic;
using IDisposable = CustomEventBus.IDisposable;

public class ServiceLocatorLoaderGame: MonoBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private GUIHolder _guiHolder;
    [SerializeField] private HUD _hud;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private LevelHolder _levelHolder;
    [SerializeField] private LineDrawer _lineDrawer;
    [SerializeField] private NavMeshSurfaceLayer _navMeshSurface;
    private GameController _gameController;
    private PauseController _pauseController;
    private LevelDataManager _levelDataManager;
    private LevelController _levelController;
    private EventBus _eventBus;
    private List<IDisposable> _disposables = new List<IDisposable>();
    
    private void Awake()
    {
        ServiceLocator.Initialize();
        _eventBus = new EventBus();
        _pauseController = new PauseController();
        _gameController = new GameController();
        _levelController = new LevelController();
        _levelDataManager = new LevelDataManager();
        Register();
        Init();
        AddDisposables();
    }
    private void Register()
    {
        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register<GUIHolder>(_guiHolder);
        ServiceLocator.Current.Register(_levelDataManager);
        ServiceLocator.Current.Register(_pauseController);
        ServiceLocator.Current.Register(_playerSpawner);
        ServiceLocator.Current.Register(_enemySpawner);
        ServiceLocator.Current.Register<LevelHolder>(_levelHolder);
    }
    private void Init()
    {
        _playerSpawner.Init();
        _enemySpawner.Init();
        _hud.Init();
        _gameController.Init();
        _navMeshSurface.Init();
        _levelController.Init();
        _lineDrawer.Init();
    }
    private void AddDisposables()
    {
        _disposables.Add(_gameController);
        _disposables.Add(_levelController);
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}
