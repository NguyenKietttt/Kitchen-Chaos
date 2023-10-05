using UnityEngine;
using UnityEngine.UI;

public sealed class GamePauseUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private Button _resumeBtn;
    [SerializeField] private Button _mainMenuBtn;

    private void Start()
    {
        _resumeBtn.onClick.AddListener(OnResumeButtonClicked);
        _mainMenuBtn.onClick.AddListener(OnMainMenuButtonClicked);

        Bootstrap.Instance.EventMgr.OnPaused += Show;
        Bootstrap.Instance.EventMgr.OnUnPaused += Hide;

        Hide();
    }

    private void OnDestroy()
    {
        _resumeBtn.onClick.RemoveAllListeners();
        _mainMenuBtn.onClick.RemoveAllListeners();

        Bootstrap.Instance.EventMgr.OnPaused -= Show;
        Bootstrap.Instance.EventMgr.OnUnPaused -= Hide;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnResumeButtonClicked()
    {
        Bootstrap.Instance.EventMgr.TooglePause?.Invoke();
    }

    public void OnMainMenuButtonClicked()
    {
        Bootstrap.Instance.EventMgr.Dispose();
        Bootstrap.Instance.SceneLoader.Load(SceneLoader.Scene.MainMenu);
    }
}
