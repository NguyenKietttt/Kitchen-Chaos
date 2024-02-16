using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "SO_RebindKey", menuName = "Scriptable Object/Rebind Key")]
    public sealed class RebindKeySO : ScriptableObject
    {
        public int Index => _index;

        [Header("Asset Ref")]
        [SerializeField] private InputActionReference _inputActionRef;

        [Header("Property")]
        [SerializeField] private int _index;

        [NonSerialized] private InputAction _inputAction;
        [NonSerialized] private bool _isInited;

        private void Init()
        {
            if (_isInited)
            {
                return;
            }

            _isInited = true;
            _inputAction = Bootstrap.Instance.InputMgr.PlayerInputAction.asset.FindAction(_inputActionRef.action.name);
        }

        public string GetActionName()
        {
            Init();
            return _inputAction.name;
        }

        public string GetDisplayString()
        {
            Init();
            return _inputAction.bindings[_index].ToDisplayString();
        }
    }
}
