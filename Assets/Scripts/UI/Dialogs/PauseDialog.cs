using UnityEngine;
using UnityEngine.UI;
using UI;
using CustomEventBus;
using CustomEventBus.Signals;

public class PauseDialog: Dialog
{
    [SerializeField] private Button _continue;
    [SerializeField] private Button _exit;
    private PauseController pauseController;
    private EventBus _eventBus;

    private void Start()
    {
        pauseController = ServiceLocator.Current.Get<PauseController>();
        pauseController.SetPaused(true);
        _continue.onClick.AddListener(Continue);
        _exit.onClick.AddListener(Exit);
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }

    private void Continue()
    {
        pauseController.SetPaused(false);
        Hide();
    }
    private void Exit()
    {
        Application.Quit();
    }
}