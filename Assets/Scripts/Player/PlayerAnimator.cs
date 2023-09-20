using UnityEngine;

public sealed class PlayerAnimator : MonoBehaviour
{
    private static readonly int IsWalkingParam = Animator.StringToHash("IsWalking");

    [SerializeField] private Animator _animator;

    private bool _lastMovingValue;

    public void UpdateWalkingAnim(bool canMove)
    {
        if (_lastMovingValue == canMove)
        {
            return;
        }

        _animator.SetBool(IsWalkingParam, canMove);
        _lastMovingValue = canMove;
    }
}
