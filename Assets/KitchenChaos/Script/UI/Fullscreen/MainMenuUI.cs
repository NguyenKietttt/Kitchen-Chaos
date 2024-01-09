using UISystem;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenuUI : BaseScreen
{
    [Header("Asset Ref")]
    [SerializeField] private GameObject _decorationPrefab;

    [Header("Internal Ref")]
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _quitBtn;

    private GameObject _decorationObj;

    public override void OnPush(object[] datas = null)
    {
        _playBtn.onClick.AddListener(OnPlayButtonClicked);
        _quitBtn.onClick.AddListener(OnQuitButtonClicked);

        _decorationObj = Instantiate(_decorationPrefab);

        _playBtn.Select();

        Time.timeScale = 1;
    }

    public override void OnFocus()
    {
        gameObject.SetActive(true);
        _decorationObj.SetActive(true);
    }

    public override void OnFocusLost()
    {
        gameObject.SetActive(false);
        _decorationObj.SetActive(false);
    }

    public override void OnPop()
    {
        _playBtn.onClick.RemoveAllListeners();
        _quitBtn.onClick.RemoveAllListeners();

        Destroy(gameObject);
        Destroy(_decorationObj);
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
