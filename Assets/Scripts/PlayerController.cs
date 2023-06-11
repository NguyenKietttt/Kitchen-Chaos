using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int SPEED = 7;

    private void Update()
    {
        Vector2 input = GetInputVector();
        UpdatePosition(input);
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

    private void UpdatePosition(Vector2 input)
    {
        input = input.normalized;
        var moveDir = new Vector3(input.x, 0, input.y);
        transform.position += moveDir * (SPEED * Time.deltaTime);
    }
}