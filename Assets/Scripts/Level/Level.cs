using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using System.Collections.Generic;

namespace Levels
{
    public class Level : MonoBehaviour, IService
    {
        public LevelData levelData;
        private EventBus _eventBus;
        private void Awake() {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<GameClearSignal>(OnDelete); 
            _eventBus.Invoke(new GetLevelDataSignal(levelData)); 
        }
        private void OnDelete(GameClearSignal signal)
        {
            Destroy(gameObject);
        }
        private void OnDestroy() {
            _eventBus.Unsubscribe<GameClearSignal>(OnDelete);
        }
    }
}
