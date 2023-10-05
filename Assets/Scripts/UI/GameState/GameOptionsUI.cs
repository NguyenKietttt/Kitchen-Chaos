using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameOptionsUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private Button _sfxVolumnBtn;
    [SerializeField] private Button _musicVolumnBtn;
    [SerializeField] private Button _closeBtn;
    [SerializeField] private TextMeshProUGUI _sfxVolumnTxt;
    [SerializeField] private TextMeshProUGUI _musicVolumnTxt;

    private readonly StringBuilder _stringBuilder = new();

    private void Start()
    {
        _sfxVolumnBtn.onClick.AddListener(OnSFXVolumnButtonClicked);
        _musicVolumnBtn.onClick.AddListener(OnMusicVolumnButtonClicked);
        _closeBtn.onClick.AddListener(Hide);

        Bootstrap.Instance.EventMgr.OnUnPaused += Hide;
        Bootstrap.Instance.EventMgr.ClickOptionsBtn += OnOptionsButtonClicked;

        _stringBuilder
            .Clear()
            .Append("Sound Effects: ")
            .Append(Mathf.Ceil(Bootstrap.Instance.SFXMgr.MasterVolumn * 10));

        _sfxVolumnTxt.SetText(_stringBuilder);

        _stringBuilder
            .Clear()
            .Append("Music: ")
            .Append(Mathf.Ceil(Bootstrap.Instance.MusicMgr.MasterVolumn * 10));

        _musicVolumnTxt.SetText(_stringBuilder);

        Hide();
    }

    private void OnDestroy()
    {
        _sfxVolumnBtn.onClick.RemoveAllListeners();
        _musicVolumnBtn.onClick.RemoveAllListeners();
        _closeBtn.onClick.RemoveAllListeners();

        Bootstrap.Instance.EventMgr.OnUnPaused -= Hide;
        Bootstrap.Instance.EventMgr.ClickOptionsBtn -= OnOptionsButtonClicked;
    }

    private void OnOptionsButtonClicked()
    {
        Show();
    }

    private void OnSFXVolumnButtonClicked()
    {
        Bootstrap.Instance.SFXMgr.ChangeVolumn();

        _stringBuilder
            .Clear()
            .Append("Sound Effects: ")
            .Append(Mathf.Ceil(Bootstrap.Instance.SFXMgr.MasterVolumn * 10));

        _sfxVolumnTxt.SetText(_stringBuilder);
    }

    private void OnMusicVolumnButtonClicked()
    {
        Bootstrap.Instance.MusicMgr.ChangeVolumn();

        _stringBuilder
            .Clear()
            .Append("Music: ")
            .Append(Mathf.Ceil(Bootstrap.Instance.MusicMgr.MasterVolumn * 10));

        _musicVolumnTxt.SetText(_stringBuilder);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
