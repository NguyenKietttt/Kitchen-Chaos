using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int MOVING_SPEED = 7;
    private const int ROTATION_SPEED = 10;

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private void Update()
    {
        Vector2 input = _inputManager.GetInputVectorNormalized();
        bool isMoving = IsMoving(input);

        if (isMoving)
        {
            Vector3 moveDir = UpdatePosition(input);
            UpdateRotationByMovingDirection(moveDir);
        }

        _playerAnimator.UpdateWalkingAnim(isMoving);
    }

    private bool IsMoving(Vector2 input)
    {
        return input != Vector2.zero;
    }

    private Vector3 UpdatePosition(Vector2 input)
    {
        var moveDir = new Vector3(input.x, 0, input.y);
        transform.position += moveDir * (MOVING_SPEED * Time.deltaTime);

        return moveDir;
    }

    private void UpdateRotationByMovingDirection(Vector3 moveDir)
    {
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * ROTATION_SPEED);
    }
}