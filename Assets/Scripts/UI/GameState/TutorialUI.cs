using TMPro;
using UnityEngine;

public sealed class TutorialUI : MonoBehaviour
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

    private void OnEnable()
    {
        Bootstrap.Instance.EventMgr.RebindingKey += UpdateVisual;
        Bootstrap.Instance.EventMgr.ChangeGameState += OnGameStateChanged;
    }

    private void Start()
    {
        Show();
        UpdateVisual();
    }

    private void OnDisable()
    {
        Bootstrap.Instance.EventMgr.RebindingKey -= UpdateVisual;
        Bootstrap.Instance.EventMgr.ChangeGameState -= OnGameStateChanged;
    }

    private void OnGameStateChanged()
    {
        if (Bootstrap.Instance.GameStateMgr.IsCounDownToStartActive)
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
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
