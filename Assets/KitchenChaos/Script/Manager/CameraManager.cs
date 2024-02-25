using Cinemachine;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class CameraManager : MonoBehaviour
    {
        [Header("External Ref")]
        [SerializeField] private CinemachineVirtualCamera _mainMenuVirtualCam;
        [SerializeField] private CinemachineVirtualCamera _gameplayVirtualCam;

        private EventManager _eventMgr;

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            _eventMgr.ChangeGameState += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            _eventMgr.ChangeGameState -= OnGameStateChanged;
            DeregisterServices();
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.MainMenu:
                    ChangeCamera(CameraState.MainMenu);
                    break;
                case GameState.WaitingToStart:
                    ChangeCamera(CameraState.Gameplay);
                    break;
            }
        }

        private void ChangeCamera(CameraState state)
        {
            switch (state)
            {
                case CameraState.MainMenu:
                    _mainMenuVirtualCam.enabled = true;
                    _gameplayVirtualCam.enabled = false;

                    break;
                case CameraState.Gameplay:
                    _mainMenuVirtualCam.enabled = false;
                    _gameplayVirtualCam.enabled = true;

                    break;
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
        }
    }
}
