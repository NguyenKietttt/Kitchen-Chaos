using System.Text;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class OptionMenuUI : BaseScreen
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
        [SerializeField] private Button _interactGamepadBtn;
        [SerializeField] private Button _cutGamepadBtn;

        [Space]

        [SerializeField] private TextMeshProUGUI _moveUpTxt;
        [SerializeField] private TextMeshProUGUI _moveDownTxt;
        [SerializeField] private TextMeshProUGUI _moveLeftTxt;
        [SerializeField] private TextMeshProUGUI _moveRightTxt;
        [SerializeField] private TextMeshProUGUI _interactTxt;
        [SerializeField] private TextMeshProUGUI _cutTxt;
        [SerializeField] private TextMeshProUGUI _interactGamepadtTxt;
        [SerializeField] private TextMeshProUGUI _cutGamepadTxt;

        private readonly StringBuilder _stringBuilder = new();

        public override void OnPush(object[] datas = null)
        {
            _sfxVolumnBtn.onClick.AddListener(OnSFXVolumnButtonClicked);
            _musicVolumnBtn.onClick.AddListener(OnMusicVolumnButtonClicked);
            _closeBtn.onClick.AddListener(OnCloseButtonClicked);

            _moveUpBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.MoveUp));
            _moveDownBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.MoveDown));
            _moveLeftBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.MoveLeft));
            _moveRightBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.MoveRight));
            _interactBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.Interact));
            _cutBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.Cut));
            _interactGamepadBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.GamepadInteract));
            _cutGamepadBtn.onClick.AddListener(() => OnRebindKeyClicked(InputManager.Binding.GamepadCut));

            UpdateSFXVolumeText();
            UpdateMusicVolume();
            UpdateRebindKeyVisuals();

            Time.timeScale = 0;
        }

        public override void OnFocus()
        {
            Time.timeScale = 0;
        }

        public override void OnFocusLost()
        {
            Time.timeScale = 1;
        }

        public override void OnPop()
        {
            _moveUpBtn.onClick.RemoveAllListeners();
            _moveDownBtn.onClick.RemoveAllListeners();
            _moveLeftBtn.onClick.RemoveAllListeners();
            _moveRightBtn.onClick.RemoveAllListeners();
            _interactBtn.onClick.RemoveAllListeners();
            _cutBtn.onClick.RemoveAllListeners();
            _interactGamepadBtn.onClick.RemoveAllListeners();
            _cutGamepadBtn.onClick.RemoveAllListeners();

            _sfxVolumnBtn.onClick.RemoveAllListeners();
            _musicVolumnBtn.onClick.RemoveAllListeners();
            _closeBtn.onClick.RemoveAllListeners();

            Destroy(gameObject);
            Time.timeScale = 1;
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

        private void OnCloseButtonClicked()
        {
            Bootstrap.Instance.UIManager.Pop();
        }

        private void OnRebindKeyClicked(InputManager.Binding binding)
        {
            Bootstrap.Instance.UIManager.Push(ScreenID.RebindKey);
            Bootstrap.Instance.InputMgr.RebindBinding(binding, () =>
            {
                Bootstrap.Instance.UIManager.Pop();
                UpdateRebindKeyVisuals();
            });
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
            _interactGamepadtTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.GamepadInteract));
            _cutGamepadTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.GamepadCut));
        }
    }
}
