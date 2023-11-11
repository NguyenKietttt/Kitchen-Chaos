using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenuUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _quitBtn;

    private void Awake()
    {
        Bootstrap.Instance.SFXMgr.Init();
        Bootstrap.Instance.GameStateMgr.Reset();
        Bootstrap.Instance.DeliveryMgr.Reset();

        _playBtn.onClick.AddListener(OnPlayButtonClicked);
        _quitBtn.onClick.AddListener(OnQuitButtonClicked);

        Time.timeScale = 1.0f;

        _playBtn.Select();
    }

    private void OnDestroy()
    {
        _playBtn.onClick.RemoveAllListeners();
        _quitBtn.onClick.RemoveAllListeners();
    }

    private void OnPlayButtonClicked()
    {
        Bootstrap.Instance.SceneLoader.Load(SceneLoader.Scene.Loading);
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
