using System;
using UnityEngine;
using UnityEngine.AI;
using CustomEventBus.Signals;
using Unity.VisualScripting;
using EventBus = CustomEventBus.EventBus;

public class Enemy : MonoBehaviour, IService{

    private Player _target;
    private NavMeshAgent agent;
    private EventBus _eventBus;
    private PauseController _pauseController;
    private void Awake()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _pauseController = ServiceLocator.Current.Get<PauseController>();
        
        _eventBus.Subscribe<GetPlayerSignal>(OnGetPlayerSignal);
        _eventBus.Subscribe<GameClearSignal>(OnDelete);
    }

    private void Update()
    {
        if (_pauseController.IsPaused)
            agent.speed = 0;
        else
            agent.speed = 3;
    }

    private void FixedUpdate() 
    {
        
        agent.SetDestination(new Vector3(_target.transform.position.x,_target.transform.position.y,0));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _eventBus.Invoke(new HitPlayerSignal());
            Destroy(gameObject);
        }
    }
    private void OnGetPlayerSignal(GetPlayerSignal signal)
    {
        _target = signal.Player;
    }
    private void OnDelete(GameClearSignal signal)
    {
        Destroy(gameObject);
    }
    private void OnDestroy() {
        _eventBus.Unsubscribe<GetPlayerSignal>(OnGetPlayerSignal);
        _eventBus.Unsubscribe<GameClearSignal>(OnDelete);
    }
}