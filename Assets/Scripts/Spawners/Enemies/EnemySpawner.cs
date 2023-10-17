using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ObjectPool;
using CustomEventBus;
using CustomEventBus.Signals;

public class EnemySpawner : MonoBehaviour, IService, IPauseHandler {
    private EventBus _eventBus;
    private List<IEnemySpawnPoint> _enemySpawns;
    private Pool<Enemy> pool;
    private List<EnemyData> _enemies;
    private bool isPaused; 
    private PauseController _pauseController; 

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _pauseController = ServiceLocator.Current.Get<PauseController>();
        _pauseController.Register(this);
        _eventBus.Subscribe<SpawnEnemiesSignal>(OnSpawnEnemies);
        _eventBus.Subscribe<ReleaseEnemySignal>(OnReleaseEnemy);
        _eventBus.Subscribe<GameClearSignal>(OnDelete);
        _eventBus.Subscribe<GetLevelDataSignal>(GetPool);
    }

    public void OnSpawnEnemies(SpawnEnemiesSignal signal)
    {
        _enemySpawns = new List<IEnemySpawnPoint>();
        _eventBus.Invoke(new RegisterEnemySpawnSignal());
        pool = new Pool<Enemy>(_enemies.ToDictionary(x=>x.typeOfEnemies, x=>x.countInSpawn*_enemySpawns.Count()));
        StartCoroutine(GetEnemies());
    }

    public void GetPool(GetLevelDataSignal signal)
    {
        _enemies = signal.LevelData.typesOfEnemies; 
    }

    public void Register(IEnemySpawnPoint spawn) => _enemySpawns.Add(spawn);

    private void OnReleaseEnemy(ReleaseEnemySignal signal)
    {
        _eventBus.Invoke(new DeadEnemySignal());
        pool.Release(signal.Enemy);
    }
    private IEnumerator GetEnemies()
    {
        for(int i=0; i<_enemies.Select(x=>x.countInSpawn).Sum(); i++)
        {
            yield return new WaitUntil(() => !isPaused);
            yield return new WaitForSeconds(1);
            foreach (var enemySpawn in _enemySpawns)
            {
                Enemy enemy = pool.Get();
                var position = enemySpawn.transform.position;
                enemy.transform.position = position;       
            }
        }
    }
    void OnDelete(GameClearSignal signal)
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<SpawnEnemiesSignal>(OnSpawnEnemies);
        _eventBus.Unsubscribe<ReleaseEnemySignal>(OnReleaseEnemy);
        _eventBus.Unsubscribe<GameClearSignal>(OnDelete);
        _eventBus.Unsubscribe<GetLevelDataSignal>(GetPool);
    }

    void IPauseHandler.SetPaused(bool IsPaused) => isPaused = IsPaused;
}