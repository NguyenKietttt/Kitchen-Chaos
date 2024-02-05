using Cinemachine;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class CameraManager : MonoBehaviour
    {
        [Header("External Ref")]
        [SerializeField] private CinemachineVirtualCamera _mainMenuVirutalCam;
        [SerializeField] private CinemachineVirtualCamera _gameplayVirutalCam;

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
                    _mainMenuVirutalCam.enabled = true;
                    _gameplayVirutalCam.enabled = false;

                    break;
                case CameraState.Gameplay:
                    _mainMenuVirutalCam.enabled = false;
                    _gameplayVirutalCam.enabled = true;

                    break;
            }
        }
    }
}
