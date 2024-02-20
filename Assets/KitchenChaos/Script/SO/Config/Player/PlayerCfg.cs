using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_Player", menuName = "Scriptable Object/Config/Player/Player")]
    public sealed class PlayerCfg : ScriptableObject
    {
        public float Radius => _radius;
        public float MoveOffset => _moveOffset;
        public int Height => _height;
        public float HeightOffset => _heightOffset;
        public int MoveSpeed => _moveSpeed;
        public int RotateSpeed => _rotateSpeed;
        public int InteractDistance => _interactDistance;

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

        public float FootstepTimerMin => _footstepTimerMin;
        public float FootstepTimerMax => _footstepTimerMax;

        public LayerMask CounterLayerMask => _counterLayerMask;

        [Header("Body")]
        [SerializeField] private float _radius;
        [SerializeField] private int _height;
        [SerializeField] private float _heightOffset;

        [Header("Movement")]
        [SerializeField] private float _moveOffset;
        [SerializeField] private int _moveSpeed;
        [SerializeField] private int _rotateSpeed;

        [Header("Interaction")]
        [SerializeField] private LayerMask _counterLayerMask;
        [SerializeField] private int _interactDistance;

        [Header("Animation")]
        [SerializeField] private string _isWalkingKey;

        [Header("SFX")]
        [SerializeField] private float _footstepTimerMin;
        [SerializeField] private float _footstepTimerMax;

        private int _isWalkingKeyHash = int.MinValue;
    }
}
