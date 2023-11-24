using UnityEngine;

public sealed class GameObjectRotation : MonoBehaviour
{
    private const float MAX_ROTATION = 360.0f;

    [Header("Property")]
    [SerializeField] private Vector3 _direction;
    [SerializeField] private bool isAnticlockwise;

    private void Update()
    {
        Rotate();

        CheckMaxRotationOnX();
        CheckMaxRotationOnY();
        CheckMaxRotationOnZ();
    }

    private void Rotate()
    {
        if (isAnticlockwise)
        {
            transform.eulerAngles -= Time.deltaTime * _direction;
        }
        else
        {
            transform.eulerAngles += Time.deltaTime * _direction;
        }
    }

    private void CheckMaxRotationOnX()
    {
        if (transform.eulerAngles.x >= MAX_ROTATION)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    private void CheckMaxRotationOnY()
    {
        if (transform.eulerAngles.y >= MAX_ROTATION)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
    }

    private void CheckMaxRotationOnZ()
    {
        if (transform.eulerAngles.z >= MAX_ROTATION)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
    }
}
