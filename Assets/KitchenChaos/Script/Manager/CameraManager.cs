using Cinemachine;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class CameraManager : MonoBehaviour
    {
        [Header("External Ref")]
        [SerializeField] private CinemachineVirtualCamera _mainMenuVirtualCam;
        [SerializeField] private CinemachineVirtualCamera _gameplayVirtualCam;

        private void Awake()
        {
            Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;
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
    }
}
