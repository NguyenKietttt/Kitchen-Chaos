using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class MainMenuUI : MonoBehaviour
{
    private const int LOADING_SCENE_INDEX = 2;

    [Header("Internal Ref")]
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _quitBtn;

    private void Awake()
    {
        _playBtn.onClick.AddListener(OnPlayButtonClicked);
        _quitBtn.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDestroy()
    {
        _playBtn.onClick.RemoveAllListeners();
        _quitBtn.onClick.RemoveAllListeners();
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(LOADING_SCENE_INDEX);
        Bootstrap.Instance.GameStateMgr.Init();
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
