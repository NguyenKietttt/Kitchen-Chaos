using System;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class InputManager : MonoBehaviour
{
    public Action OnInteractAction;

    private PlayerInputAction _playerInputAction;

    private void Awake()
    {
        InitPlayerInputAction();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
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
    }

    private void UnsubscribeEvents()
    {
        _playerInputAction.Player.Interact.performed -= OnInteractPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke();
    }
}