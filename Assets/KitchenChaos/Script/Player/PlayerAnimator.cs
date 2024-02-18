using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlayerAnimator : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlayerCfg _config;

        [Header("Internal Ref")]
        [SerializeField] private Animator _animator;

        private bool _lastMovingStatus;

        public void UpdateWalkingAnim(bool canMove)
        {
            if (_lastMovingStatus == canMove)
            {
                return;
            }

            _animator.SetBool(_config.IsWalkingKeyHash, canMove);
            _lastMovingStatus = canMove;
        }
    }
}
