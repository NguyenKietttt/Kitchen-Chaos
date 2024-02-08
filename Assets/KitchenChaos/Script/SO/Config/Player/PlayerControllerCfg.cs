using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_PlayerController", menuName = "Scriptable Object/Config/Player/PlayerController")]
    public sealed class PlayerControllerCfg : ScriptableObject
    {
        public float Radius => _radius;
        public float MoveOffset => _moveOffset;
        public int Height => _height;
        public float HeightOffset => _heightOffset;
        public int MoveSpeed => _moveSpeed;
        public int RotateSpeed => _rotateSpeed;
        public int InteractDistance => _interactDistance;

        public LayerMask CounterLayerMask => _counterLayerMask;

        [Header("Property")]
        [SerializeField] private float _radius;
        [SerializeField] private float _moveOffset;
        [SerializeField] private float _heightOffset;
        [SerializeField] private int _height;
        [SerializeField] private int _moveSpeed;
        [SerializeField] private int _rotateSpeed;
        [SerializeField] private int _interactDistance;

        [Space]

        [SerializeField] private LayerMask _counterLayerMask;
    }
}
