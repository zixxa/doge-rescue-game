using System;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using NavMeshPlus.Components;

public class NavMeshSurfaceLayer: MonoBehaviour
{
    private EventBus _eventBus;
   	public NavMeshSurface surface2D; 

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GameStartSignal>(Bake);
    }

    private void Bake(GameStartSignal signal)
    {
    }

    void Start()
    {
       surface2D.BuildNavMeshAsync();
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<GameStartSignal>(Bake);
    }
}