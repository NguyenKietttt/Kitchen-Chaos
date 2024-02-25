using System.Text;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class OptionMenuUI : BaseScreen
    {
        private const int ROUND_TO_ONE_MULTIPLICAND = 10;

        [Header("Internal Ref")]
        [SerializeField] private Button _sfxVolumeBtn;
        [SerializeField] private Button _musicVolumeBtn;
        [SerializeField] private Button _closeBtn;

        [Space]

        [SerializeField] private TextMeshProUGUI _sfxVolumeTxt;
        [SerializeField] private TextMeshProUGUI _musicVolumeTxt;

        private UIManager _uiMgr;
        private SFXManager _sfxMgr;
        private MusicManager _musicMgr;

        private readonly StringBuilder _stringBuilder = new();

        public override void OnPush(object[] datas = null)
        {
            RegisterServices();
            SubscribeEvents();

            UpdateSFXVolumeText();
            UpdateMusicVolume();

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
            UnsubscribeEvents();
            DeregisterServices();

            Destroy(gameObject);
            Time.timeScale = 1;
        }

        private void OnSFXVolumeButtonClicked()
        {
            _sfxMgr.ChangeVolume();
            UpdateSFXVolumeText();
        }

        private void OnMusicVolumeButtonClicked()
        {
            _musicMgr.ChangeVolume();
            UpdateMusicVolume();
        }

        private void OnCloseButtonClicked()
        {
            _uiMgr.Pop();
        }

        private void UpdateSFXVolumeText()
        {
            _stringBuilder
                .Clear()
                .Append("Sound Effects: ")
                .Append(Mathf.Ceil(_sfxMgr.MasterVolume * ROUND_TO_ONE_MULTIPLICAND));

            _sfxVolumeTxt.SetText(_stringBuilder);
        }

        private void UpdateMusicVolume()
        {
            _stringBuilder
                .Clear()
                .Append("Music: ")
                .Append(Mathf.Ceil(_musicMgr.MasterVolume * ROUND_TO_ONE_MULTIPLICAND));

            _musicVolumeTxt.SetText(_stringBuilder);
        }

        private void RegisterServices()
        {
            _uiMgr = ServiceLocator.Instance.Get<UIManager>();
            _sfxMgr = ServiceLocator.Instance.Get<SFXManager>();
            _musicMgr = ServiceLocator.Instance.Get<MusicManager>();
        }

        private void DeregisterServices()
        {
            _uiMgr = null;
            _sfxMgr = null;
            _musicMgr = null;
        }

        private void SubscribeEvents()
        {
            _sfxVolumeBtn.onClick.AddListener(OnSFXVolumeButtonClicked);
            _musicVolumeBtn.onClick.AddListener(OnMusicVolumeButtonClicked);
            _closeBtn.onClick.AddListener(OnCloseButtonClicked);
        }

        private void UnsubscribeEvents()
        {
            _sfxVolumeBtn.onClick.RemoveAllListeners();
            _musicVolumeBtn.onClick.RemoveAllListeners();
            _closeBtn.onClick.RemoveAllListeners();
        }
    }
}
