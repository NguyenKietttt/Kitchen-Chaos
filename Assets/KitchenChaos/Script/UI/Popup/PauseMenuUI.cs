using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class PauseMenuUI : BaseScreen
    {
        [Header("Internal Ref")]
        [SerializeField] private Button _resumeBtn;
        [SerializeField] private Button _optionsBtn;
        [SerializeField] private Button _mainMenuBtn;

        public override void OnPush(object[] datas = null)
        {
            _resumeBtn.onClick.AddListener(OnResumeButtonClicked);
            _optionsBtn.onClick.AddListener(OnOptionsButtonClicked);
            _mainMenuBtn.onClick.AddListener(OnMainMenuButtonClicked);

            Time.timeScale = 0;
        }

        public override void OnFocus()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public override void OnFocusLost()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public override void OnPop()
        {
            _resumeBtn.onClick.RemoveAllListeners();
            _optionsBtn.onClick.RemoveAllListeners();
            _mainMenuBtn.onClick.RemoveAllListeners();

            Destroy(gameObject);
            Time.timeScale = 1;
        }

        private void OnResumeButtonClicked()
        {
            Bootstrap.Instance.EventMgr.TooglePause?.Invoke();
        }

        private void OnOptionsButtonClicked()
        {
            Bootstrap.Instance.UIManager.Push(ScreenID.Option);
        }

        private void OnMainMenuButtonClicked()
        {
            Bootstrap.Instance.GameStateMgr.ChangeState(GameState.MainMenu);
        }
    }
}
