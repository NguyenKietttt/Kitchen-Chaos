using UnityEngine;
using UnityEngine.InputSystem;

public sealed class InputManager
{
    private PlayerInputAction _playerInputAction;

    public InputManager()
    {
        InitPlayerInputAction();
        SubscribeEvents();
    }

    public void OnDestroy()
    {
        UnsubscribeEvents();
        _playerInputAction.Dispose();
    }

    public Vector2 GetInputVectorNormalized()
    {
        return _playerInputAction.Player.Move.ReadValue<Vector2>();
    }

    private void InitPlayerInputAction()
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Enable();
    }

    private void SubscribeEvents()
    {
        _playerInputAction.Player.Interact.performed += OnInteractPerformed;
        _playerInputAction.Player.CuttingInteract.performed += OnCuttingInteractPerformed;
        _playerInputAction.Player.Pause.performed += OnPausePerformed;
    }

    private void UnsubscribeEvents()
    {
        _playerInputAction.Player.Interact.performed -= OnInteractPerformed;
        _playerInputAction.Player.CuttingInteract.performed -= OnCuttingInteractPerformed;
        _playerInputAction.Player.Pause.performed -= OnPausePerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        Bootstrap.Instance.EventMgr.Interact?.Invoke();
    }

    private void OnCuttingInteractPerformed(InputAction.CallbackContext obj)
    {
        Bootstrap.Instance.EventMgr.CuttingInteract?.Invoke();
    }

    private void OnPausePerformed(InputAction.CallbackContext obj)
    {
        Bootstrap.Instance.EventMgr.TooglePause?.Invoke();
    }
}
