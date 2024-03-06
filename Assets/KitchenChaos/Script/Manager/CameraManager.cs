using Cinemachine;
using KitchenChaos.Utils;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class CameraManager : MonoBehaviour
    {
        [Header("External Ref")]
        [SerializeField] private CinemachineVirtualCamera? _mainMenuVirtualCam;
        [SerializeField] private CinemachineVirtualCamera? _gameplayVirtualCam;

        private EventManager? _eventMgr;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
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
                    _mainMenuVirtualCam!.enabled = true;
                    _gameplayVirtualCam!.enabled = false;

                    break;
                case CameraState.Gameplay:
                    _mainMenuVirtualCam!.enabled = false;
                    _gameplayVirtualCam!.enabled = true;

                    break;
            }
        }

        private void CheckNullEditorReferences()
        {
            if (_mainMenuVirtualCam == null || _gameplayVirtualCam == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
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

        private void SubscribeEvents()
        {
            _eventMgr!.ChangeGameState += OnGameStateChanged;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.ChangeGameState -= OnGameStateChanged;
        }
    }
}
