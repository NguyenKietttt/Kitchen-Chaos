using TMPro;
using UISystem;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class TutorialUI : BaseScreen
    {
        [Header("Internal Ref")]
        [SerializeField] private TextMeshProUGUI _keyboardMoveUpTxt;
        [SerializeField] private TextMeshProUGUI _keyboardMoveDownTxt;
        [SerializeField] private TextMeshProUGUI _keyboardMoveLeftTxt;
        [SerializeField] private TextMeshProUGUI _keyboardMoveRightTxt;
        [SerializeField] private TextMeshProUGUI _keyboardInteractTxt;
        [SerializeField] private TextMeshProUGUI _keyboardCutTxt;
        [SerializeField] private TextMeshProUGUI _gamepadInteractTxt;
        [SerializeField] private TextMeshProUGUI _gamepadCutTxt;

        public override void OnPush(object[] datas = null)
        {
            Bootstrap.Instance.EventMgr.RebindingKey += UpdateVisual;
            Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;

            UpdateVisual();
        }

        public override void OnFocus()
        {
            gameObject.SetActive(true);
        }

        public override void OnFocusLost()
        {
            gameObject.SetActive(false);
        }

        public override void OnPop()
        {
            Bootstrap.Instance.EventMgr.RebindingKey -= UpdateVisual;
            Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;

            Destroy(gameObject);
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state is GameState.CountDownToStart)
            {
                Bootstrap.Instance.UIManager.Pop();
            }
        }

        private void UpdateVisual()
        {
            _keyboardMoveUpTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.MoveUp));
            _keyboardMoveDownTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.MoveDown));
            _keyboardMoveLeftTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.MoveLeft));
            _keyboardMoveRightTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.MoveRight));
            _keyboardInteractTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.Interact));
            _keyboardCutTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.Cut));

            _gamepadInteractTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.GamepadInteract));
            _gamepadCutTxt.SetText(Bootstrap.Instance.InputMgr.GetBidingText(InputManager.Binding.GamepadCut));
        }
    }
}
