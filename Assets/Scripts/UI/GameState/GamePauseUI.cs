using UnityEngine;
using UnityEngine.UI;

public sealed class GamePauseUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private Button _resumeBtn;
    [SerializeField] private Button _optionsBtn;
    [SerializeField] private Button _mainMenuBtn;

    private void Start()
    {
        _resumeBtn.onClick.AddListener(OnResumeButtonClicked);
        _optionsBtn.onClick.AddListener(OnOptionsButtonClicked);
        _mainMenuBtn.onClick.AddListener(OnMainMenuButtonClicked);

        Bootstrap.Instance.EventMgr.OnPaused += Show;
        Bootstrap.Instance.EventMgr.CloseOptionUI += Show;
        Bootstrap.Instance.EventMgr.OnUnPaused += Hide;

        Hide();
    }

    private void OnDestroy()
    {
        _resumeBtn.onClick.RemoveAllListeners();
        _optionsBtn.onClick.RemoveAllListeners();
        _mainMenuBtn.onClick.RemoveAllListeners();

        Bootstrap.Instance.EventMgr.OnPaused -= Show;
        Bootstrap.Instance.EventMgr.CloseOptionUI -= Show;
        Bootstrap.Instance.EventMgr.OnUnPaused -= Hide;
    }

    private void Show()
    {
        gameObject.SetActive(true);
        _resumeBtn.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnResumeButtonClicked()
    {
        Bootstrap.Instance.EventMgr.TooglePause?.Invoke();
    }

    public void OnOptionsButtonClicked()
    {
        Bootstrap.Instance.EventMgr.ClickOptionsBtn?.Invoke();
        Hide();
    }

    public void OnMainMenuButtonClicked()
    {
        Bootstrap.Instance.EventMgr.Dispose();
        Bootstrap.Instance.SceneLoader.Load(SceneLoader.Scene.MainMenu);
    }
}
