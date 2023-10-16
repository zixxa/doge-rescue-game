using System;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public interface IEnemySpawnPoint
{
    public Transform transform{get;}
}
public class EnemySpawnPoint : MonoBehaviour, IService, IEnemySpawnPoint{
    private EventBus _eventBus;
    private EnemySpawner _enemySpawner;
    public Transform transform => gameObject.transform;
    void Awake()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _enemySpawner = ServiceLocator.Current.Get<EnemySpawner>();
        _eventBus.Subscribe<RegisterEnemySpawnSignal>(OnInit);
    }
    void OnInit(RegisterEnemySpawnSignal signal)
    {
        _enemySpawner.Register(this);
    }
    private void OnDestroy()
    {
        _eventBus.Unsubscribe<RegisterEnemySpawnSignal>(OnInit);
    }
}