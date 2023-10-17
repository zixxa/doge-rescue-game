using System;
using UnityEngine;
using CustomEventBus.Signals;
using CustomEventBus;

public class Player: MonoBehaviour, IService
{
    private EventBus _eventBus;
    private int bodyCount;
    private int hp = 3;
    public int Hp => hp;    

    
    void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<SpawnEnemiesSignal>(GetPosition);
        _eventBus.Subscribe<GameClearSignal>(OnClear);
        _eventBus.Subscribe<HitPlayerSignal>(HitPlayer);
        
        _eventBus.Invoke(new RedrawHPSignal(hp));
    }

    private void GetPosition(SpawnEnemiesSignal signal) => _eventBus.Invoke(new GetPlayerSignal(this));

    private void HitPlayer(HitPlayerSignal signal)
    {
        hp--;
        _eventBus.Invoke(new RedrawHPSignal(hp));
    }

    void FixedUpdate(){
        if (Hp<=0)
            _eventBus.Invoke(new GameOverSignal());
    }
    private void OnClear(GameClearSignal signal) => Destroy(this.gameObject);

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<SpawnEnemiesSignal>(GetPosition);
        _eventBus.Unsubscribe<GameClearSignal>(OnClear);
        _eventBus.Unsubscribe<HitPlayerSignal>(HitPlayer);
    }
}
