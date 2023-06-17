using UnityEngine;

public sealed class PlayerAnimator : MonoBehaviour
{
    private static readonly int IsWalkingParam = Animator.StringToHash("IsWalking");

    [SerializeField] private Animator _animator;

    private bool _lastMovingValue;

    public void UpdateWalkingAnim(bool isWalking)
    {
        if (_lastMovingValue == isWalking)
        {
            return;
        }

        _animator.SetBool(IsWalkingParam, isWalking);
        _lastMovingValue = isWalking;
    }
}