using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class GamePlayingClockUI : MonoBehaviour
    {
        [Header("Internal Ref")]
        [SerializeField] private Image _timerImg;

        private GameStateManager _gameStateMgr;

        private void Awake()
        {
            RegisterServices();
        }

        private void Update()
        {
            _timerImg.fillAmount = _gameStateMgr.PlayingTimerNormalized;
        }

        private void OnDestroy()
        {
            DeregisterServices();
        }

        private void RegisterServices()
        {
            _gameStateMgr = ServiceLocator.Instance.Get<GameStateManager>();
        }

        private void DeregisterServices()
        {
            _gameStateMgr = null;
        }
    }
}
