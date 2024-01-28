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
        [SerializeField] private Button _sfxVolumnBtn;
        [SerializeField] private Button _musicVolumnBtn;
        [SerializeField] private Button _closeBtn;

        [Space]

        [SerializeField] private TextMeshProUGUI _sfxVolumnTxt;
        [SerializeField] private TextMeshProUGUI _musicVolumnTxt;

        private readonly StringBuilder _stringBuilder = new();

        public override void OnPush(object[] datas = null)
        {
            _sfxVolumnBtn.onClick.AddListener(OnSFXVolumnButtonClicked);
            _musicVolumnBtn.onClick.AddListener(OnMusicVolumnButtonClicked);
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

        private void UpdateSFXVolumeText()
        {
            _stringBuilder
                .Clear()
                .Append("Sound Effects: ")
                .Append(Mathf.Ceil(Bootstrap.Instance.SFXMgr.MasterVolumn * ROUND_TO_ONE_MULTIPLICAND));

            _sfxVolumnTxt.SetText(_stringBuilder);
        }

        private void UpdateMusicVolume()
        {
            _stringBuilder
               .Clear()
               .Append("Music: ")
               .Append(Mathf.Ceil(Bootstrap.Instance.MusicMgr.MasterVolumn * ROUND_TO_ONE_MULTIPLICAND));

            _musicVolumnTxt.SetText(_stringBuilder);
        }
    }
}
