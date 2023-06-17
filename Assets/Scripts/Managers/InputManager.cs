using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputAction _playerInputAction;

    private void Start()
    {
        InitPlayerInputAction();
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
}