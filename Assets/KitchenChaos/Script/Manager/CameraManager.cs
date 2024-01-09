using Cinemachine;
using UnityEngine;

public sealed class CameraManager : MonoBehaviour
{
    public enum CameraState { None, MainMenu, Gameplay }

    public static CameraManager Instance => _instance;
    private static CameraManager _instance;

    [Header("External Ref")]
    [SerializeField] private CinemachineVirtualCamera _mainMenuVirutalCam;
    [SerializeField] private CinemachineVirtualCamera _gameplayVirutalCam;

    private void Awake()
    {
        _instance = this;

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
            case GameState.CountDownToStart:
                break;
            case GameState.GamePlaying:
                break;
            case GameState.GameOver:
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
