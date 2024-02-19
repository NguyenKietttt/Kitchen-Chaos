using System.Text;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

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

        private readonly StringBuilder _stringBuilder = new();

        public override void OnPush(object[] datas = null)
        {
            _sfxVolumeBtn.onClick.AddListener(OnSFXVolumeButtonClicked);
            _musicVolumeBtn.onClick.AddListener(OnMusicVolumeButtonClicked);
            _closeBtn.onClick.AddListener(OnCloseButtonClicked);

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
            _sfxVolumeBtn.onClick.RemoveAllListeners();
            _musicVolumeBtn.onClick.RemoveAllListeners();
            _closeBtn.onClick.RemoveAllListeners();

            Destroy(gameObject);
            Time.timeScale = 1;
        }

        private void OnSFXVolumeButtonClicked()
        {
            Bootstrap.Instance.SFXMgr.ChangeVolume();
            UpdateSFXVolumeText();
        }

        private void OnMusicVolumeButtonClicked()
        {
            Bootstrap.Instance.MusicMgr.ChangeVolume();
            UpdateMusicVolume();
        }

        private void OnCloseButtonClicked()
        {
            Bootstrap.Instance.UIManager.Pop();
        }

        private void UpdateSFXVolumeText()
        {
            _stringBuilder
                .Clear()
                .Append("Sound Effects: ")
                .Append(Mathf.Ceil(Bootstrap.Instance.SFXMgr.MasterVolume * ROUND_TO_ONE_MULTIPLICAND));

            _sfxVolumeTxt.SetText(_stringBuilder);
        }

        private void UpdateMusicVolume()
        {
            _stringBuilder
                .Clear()
                .Append("Music: ")
                .Append(Mathf.Ceil(Bootstrap.Instance.MusicMgr.MasterVolume * ROUND_TO_ONE_MULTIPLICAND));

            _musicVolumeTxt.SetText(_stringBuilder);
        }
    }
}
