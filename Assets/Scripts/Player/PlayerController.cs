using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    private const float PLAYER_RADIUS = 0.7f;
    private const int PLAYER_HEIGHT = 2;
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
            var moveDir = new Vector3(input.x, 0, input.y);
            float moveDistance = MOVING_SPEED * Time.deltaTime;

            if (!CanMove(moveDir, moveDistance))
            {
                moveDir = TryMoveHugWall(moveDir, moveDistance);
            }

            if (CanMove(moveDir, moveDistance))
            {
                UpdatePosition(moveDir, moveDistance);
            }

            UpdateRotationByMovingDirection(moveDir);
        }

        _playerAnimator.UpdateWalkingAnim(isMoving);
    }

    private Vector3 TryMoveHugWall(Vector3 moveDir, float moveDistance)
    {
        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

        if (CanMove(moveDirX, moveDistance))
        {
            return moveDirX;
        }

        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;

        if (CanMove(moveDirZ, moveDistance))
        {
            return moveDirZ;
        }

        return Vector3.zero;
    }

    private bool CanMove(Vector3 moveDir, float moveDistance)
    {
        Vector3 curPos = transform.position;
        return !Physics.CapsuleCast(curPos, curPos + Vector3.up * PLAYER_HEIGHT, PLAYER_RADIUS, moveDir, moveDistance);
    }

    private bool IsMoving(Vector2 input)
    {
        return input != Vector2.zero;
    }

    private void UpdatePosition(Vector3 moveDir, float moveDistance)
    {
        transform.position += moveDir * moveDistance;
    }

    private void UpdateRotationByMovingDirection(Vector3 moveDir)
    {
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * ROTATION_SPEED);
    }
}