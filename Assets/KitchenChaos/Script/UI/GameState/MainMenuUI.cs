using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenuUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _quitBtn;

    private void Awake()
    {
        _playBtn.onClick.AddListener(OnPlayButtonClicked);
        _quitBtn.onClick.AddListener(OnQuitButtonClicked);
    }

    private void Start()
    {
        _playBtn.Select();
    }

    private void OnDestroy()
    {
        _playBtn.onClick.RemoveAllListeners();
        _quitBtn.onClick.RemoveAllListeners();
    }

    private void OnPlayButtonClicked()
    {
        Bootstrap.Instance.GameStateMgr.ChangeState(GameState.WaitingToStart);
    }

    private void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
