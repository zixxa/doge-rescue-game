using CustomEventBus;
using UnityEngine;
using UI;
using Levels;
using System.Collections.Generic;
using IDisposable = CustomEventBus.IDisposable;

public class ServiceLocatorLoaderMenu: MonoBehaviour
{
    [SerializeField] private GUIHolder _guiHolder;
    private EventBus _eventBus;
    private List<IDisposable> _disposables = new List<IDisposable>();
    
    private void Awake()
    {
        ServiceLocator.Initialize();
        _eventBus = new EventBus();
        Register();
        Init();
        AddDisposables();
        
    }
    private void Register()
    {
        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register<GUIHolder>(_guiHolder);
    }
    private void Init()
    {
    }
    private void AddDisposables()
    {
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}
