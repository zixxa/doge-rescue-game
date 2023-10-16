using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
public interface IPlayerSpawnPoint
{
    public Transform transform {get;}
}

public class PlayerSpawnPoint : MonoBehaviour, IService ,IPlayerSpawnPoint{
    private EventBus _eventBus;
    private PlayerSpawner _playerSpawner;
    public Transform transform => gameObject.transform;
    void Awake()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _playerSpawner = ServiceLocator.Current.Get<PlayerSpawner>();
        
        _eventBus.Subscribe<RegisterPlayerSpawnSignal>(OnInit);
        _eventBus.Subscribe<GameClearSignal>(OnUnsubscribe);
    }
    private void OnInit(RegisterPlayerSpawnSignal signal)
    {
        _playerSpawner.RegisterSpawn(this);
    }
    void OnUnsubscribe(GameClearSignal signal)
    {
        _eventBus.Unsubscribe<RegisterPlayerSpawnSignal>(OnInit);
    }
}