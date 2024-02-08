using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_PlayerAnimator", menuName = "Scriptable Object/Config/Player/PlayerAnimator")]
    public sealed class PlayerAnimatorCfg : ScriptableObject
    {
        public int IsWalkingKeyHash
        {
            get
            {
                if (_isWalkingKeyHash <= int.MinValue)
                {
                    _isWalkingKeyHash = Animator.StringToHash(_isWalkingKey);
                }

                return _isWalkingKeyHash;
            }
        }

        [Header("Property")]
        [SerializeField] private string _isWalkingKey;

        private int _isWalkingKeyHash = int.MinValue;
    }
}
