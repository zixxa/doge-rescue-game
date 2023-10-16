using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using CustomEventBus.Signals;
using UI;
using UnityEngine.UI;
using EventBus = CustomEventBus.EventBus;

public class HUD: MonoBehaviour, IService
{
    private int time = 10;
    private EventBus _eventBus;
    private PauseController _pauseController;
    [SerializeField] private Button pause;
    [SerializeField] private Slider timerBar;
    [SerializeField] private List<HPStar> HP;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _pauseController = ServiceLocator.Current.Get<PauseController>();
        _eventBus.Subscribe<SpawnEnemiesSignal>(StartCountdown);
        pause.onClick.AddListener(OnPause);
        foreach (var hp in HP)
        {
            hp.Init();
        }
        
        timerBar.value = time;
    }

    private void OnPause()
    {
        DialogManager.ShowDialog<PauseDialog>();
    }

    private void StartCountdown(SpawnEnemiesSignal signal) => StartCoroutine(Countdown());
    private IEnumerator Countdown()
    {
        while(timerBar.value>0)
        {
            yield return new WaitUntil(() => !_pauseController.IsPaused);
            yield return new WaitForSeconds(1);
            timerBar.value--;
        }
        _eventBus.Invoke(new LevelPassedSignal());
        timerBar.value = time;
    }
    

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<SpawnEnemiesSignal>(StartCountdown);
    }
}
