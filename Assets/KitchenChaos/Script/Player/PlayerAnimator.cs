using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlayerAnimator : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlayerCfg? _config;

        [Header("Internal Ref")]
        [SerializeField] private Animator? _animator;

        private bool _lastMovingStatus;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        public void UpdateWalkingAnim(bool canMove)
        {
            if (_lastMovingStatus == canMove)
            {
                return;
            }

            _animator!.SetBool(_config!.IsWalkingKeyHash, canMove);
            _lastMovingStatus = canMove;
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null || _animator == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }
    }
}
