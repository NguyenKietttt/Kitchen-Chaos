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
    }

    private void UnsubscribeEvents()
    {
        _playerInputAction.Player.Interact.performed -= OnInteractPerformed;
        _playerInputAction.Player.CuttingInteract.performed -= OnCuttingInteractPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        Bootstrap.Instance.EventMgr.Interact?.Invoke();
    }

    private void OnCuttingInteractPerformed(InputAction.CallbackContext obj)
    {
        Bootstrap.Instance.EventMgr.CuttingInteract?.Invoke();
    }
}
