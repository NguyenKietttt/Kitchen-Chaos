using System;
using UnityEngine;

public sealed class UIManager : MonoBehaviour
{
    public static UIManager Instance => _instance;
    private static UIManager _instance;

    [Header("External Ref")]
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _deliveryUI;
    [SerializeField] private GameObject _playingClockUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _gamePauseUI;
    [SerializeField] private GameObject _optionUI;
    [SerializeField] private GameObject _tutorialUI;

    [Space]
    [SerializeField] private GameObject _introUIDecoration;
    [SerializeField] private GameObject _levelObj;
    [SerializeField] private GameObject _playerObj;

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
                _mainMenuUI.SetActive(true);
                _introUIDecoration.SetActive(true);

                _deliveryUI.SetActive(false);
                _playingClockUI.SetActive(false);
                _gameOverUI.SetActive(false);
                _gamePauseUI.SetActive(false);
                _optionUI.SetActive(false);
                _tutorialUI.SetActive(false);
                _levelObj.SetActive(false);
                _playerObj.SetActive(false);

                break;
            case GameState.WaitingToStart:
                _deliveryUI.SetActive(true);
                _playingClockUI.SetActive(true);
                _tutorialUI.SetActive(true);
                _levelObj.SetActive(true);
                _playerObj.SetActive(true);

                _mainMenuUI.SetActive(false);
                _introUIDecoration.SetActive(false);
                _gameOverUI.SetActive(false);
                _gamePauseUI.SetActive(false);
                _optionUI.SetActive(false);

                break;
            case GameState.CountDownToStart:
                break;
            case GameState.GamePlaying:
                break;
            case GameState.GameOver:
                break;
        }
    }
}
