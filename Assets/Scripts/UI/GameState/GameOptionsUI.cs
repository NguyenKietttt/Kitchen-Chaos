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

    [Space]

    [SerializeField] private TextMeshProUGUI _sfxVolumnTxt;
    [SerializeField] private TextMeshProUGUI _musicVolumnTxt;

    [Space]

    [SerializeField] private Button _moveUpBtn;
    [SerializeField] private Button _moveDownBtn;
    [SerializeField] private Button _moveLeftBtn;
    [SerializeField] private Button _moveRightBtn;
    [SerializeField] private Button _interactBtn;
    [SerializeField] private Button _cutBtn;

    [Space]

    [SerializeField] private TextMeshProUGUI _moveUpTxt;
    [SerializeField] private TextMeshProUGUI _moveDownTxt;
    [SerializeField] private TextMeshProUGUI _moveLeftTxt;
    [SerializeField] private TextMeshProUGUI _moveRightTxt;
    [SerializeField] private TextMeshProUGUI _interactTxt;
    [SerializeField] private TextMeshProUGUI _cutTxt;

    [Space]

    [SerializeField] private GameObject _rebindKeyUI;

    private readonly StringBuilder _stringBuilder = new();

    private void Start()
    {
        _sfxVolumnBtn.onClick.AddListener(OnSFXVolumnButtonClicked);
        _musicVolumnBtn.onClick.AddListener(OnMusicVolumnButtonClicked);
        _closeBtn.onClick.AddListener(Hide);

        _moveUpBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.MoveUp));
        _moveDownBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.MoveDown));
        _moveLeftBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.MoveLeft));
        _moveRightBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.MoveRight));
        _interactBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.Interact));
        _cutBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.Cut));

        Bootstrap.Instance.EventMgr.OnUnPaused += Hide;
        Bootstrap.Instance.EventMgr.ClickOptionsBtn += OnOptionsButtonClicked;

        UpdateSFXVolumeText();
        UpdateMusicVolume();

        UpdateRebindKeyVisuals();

        HideRebindKeyUI();
        Hide();
    }

    private void OnDestroy()
    {
        _moveUpBtn.onClick.RemoveAllListeners();
        _moveDownBtn.onClick.RemoveAllListeners();
        _moveLeftBtn.onClick.RemoveAllListeners();
        _moveRightBtn.onClick.RemoveAllListeners();
        _interactBtn.onClick.RemoveAllListeners();
        _cutBtn.onClick.RemoveAllListeners();

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
        UpdateSFXVolumeText();
    }

    private void OnMusicVolumnButtonClicked()
    {
        Bootstrap.Instance.MusicMgr.ChangeVolumn();
        UpdateMusicVolume();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateSFXVolumeText()
    {
        _stringBuilder
                    .Clear()
                    .Append("Sound Effects: ")
                    .Append(Mathf.Ceil(Bootstrap.Instance.SFXMgr.MasterVolumn * 10));

        _sfxVolumnTxt.SetText(_stringBuilder);
    }

    private void UpdateMusicVolume()
    {
        _stringBuilder
           .Clear()
           .Append("Music: ")
           .Append(Mathf.Ceil(Bootstrap.Instance.MusicMgr.MasterVolumn * 10));

        _musicVolumnTxt.SetText(_stringBuilder);
    }

    private void UpdateRebindKeyVisuals()
    {
        _moveUpTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.MoveUp));
        _moveDownTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.MoveDown));
        _moveLeftTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.MoveLeft));
        _moveRightTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.MoveRight));
        _interactTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.Interact));
        _cutTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.Cut));
    }

    private void OnRebindKeyClicked(InputManager.Binding binding)
    {
        ShowRebindKeyUI();
        Bootstrap.Instance.InputMgr.RebindBinding(binding, () =>
        {
            HideRebindKeyUI();
            UpdateRebindKeyVisuals();
        });
    }

    private void ShowRebindKeyUI()
    {
        _rebindKeyUI.SetActive(true);
    }

    private void HideRebindKeyUI()
    {
        _rebindKeyUI.SetActive(false);
    }
}
