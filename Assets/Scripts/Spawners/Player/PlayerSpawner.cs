using System;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using System.Collections.Generic;
using System.Linq;

public class PlayerSpawner: MonoBehaviour, IService, IPauseHandler
{
    private EventBus _eventBus;
    private GameObject snakeObj;
    private Player _player;
    private IPlayerSpawnPoint _playerSpawn;
    private int _snakeHeadId;
    private bool isPaused;
    
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GameStartSignal>(Spawn);
        _eventBus.Subscribe<GetLevelDataSignal>(GetPrefab);
    }

    private void GetPrefab(GetLevelDataSignal signal)
    {
        _player= signal.LevelData.player.prefab;
    }
    private void Spawn(GameStartSignal signal)
    { 
        _eventBus.Invoke(new RegisterPlayerSpawnSignal());
    }
    
    public void RegisterSpawn(IPlayerSpawnPoint spawn)
    {
        _playerSpawn = spawn;
        Instantiate(_player, spawn.transform.position, Quaternion.identity); 
    }
    private void OnDestroy() => _eventBus.Unsubscribe<GameStartSignal>(Spawn);
    void IPauseHandler.SetPaused(bool IsPaused) => isPaused = IsPaused;

    private void Update()
    {
        if (isPaused)
            return;
    }
}