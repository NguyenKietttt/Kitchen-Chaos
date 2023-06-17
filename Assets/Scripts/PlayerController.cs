using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int MOVING_SPEED = 7;
    private const int ROTATION_SPEED = 10;

    [SerializeField] private PlayerAnimator _playerAnimator;

    private void Update()
    {
        Vector2 input = GetInputVector();
        bool isMoving = IsMoving(input);

        if (isMoving)
        {
            Vector3 moveDir = UpdatePosition(input);
            UpdateRotationByMovingDirection(moveDir);
        }

        _playerAnimator.UpdateWalkingAnim(isMoving);
    }

    private static bool IsMoving(Vector2 input)
    {
        return input != Vector2.zero;
    }

    private static Vector2 GetInputVector()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            input.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            input.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            input.y -= 1;
        }

        return input;
    }

    private Vector3 UpdatePosition(Vector2 input)
    {
        input = input.normalized;

        var moveDir = new Vector3(input.x, 0, input.y);
        transform.position += moveDir * (MOVING_SPEED * Time.deltaTime);

        return moveDir;
    }

    private void UpdateRotationByMovingDirection(Vector3 moveDir)
    {
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * ROTATION_SPEED);
    }
}