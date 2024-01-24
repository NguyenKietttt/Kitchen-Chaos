using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlayerAnimator : MonoBehaviour
    {
        private readonly int _isWalkingAnimKey = Animator.StringToHash("IsWalking");

        [Header("Internal Ref")]
        [SerializeField] private Animator _animator;

        private bool _lastMovingStatus;

        public void UpdateWalkingAnim(bool canMove)
        {
            if (_lastMovingStatus == canMove)
            {
                return;
            }

            _animator.SetBool(_isWalkingAnimKey, canMove);
            _lastMovingStatus = canMove;
        }
    }
}
