using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

public enum HPLevel
{
    First = 1,
    Second = 2,
    Third = 3
}
public class HPStar: MonoBehaviour
{
    private EventBus _eventBus;
    [SerializeField] private HPLevel HPLevel;
    
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<RedrawHPSignal>(RedrawHP);
    }

    private void RedrawHP(RedrawHPSignal signal) => gameObject.SetActive(signal.HP >= (int)HPLevel);

}
