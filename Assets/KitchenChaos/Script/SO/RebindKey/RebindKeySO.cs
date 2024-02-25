using UnityEngine;
using UnityEngine.InputSystem;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "SO_RebindKey", menuName = "Scriptable Object/Rebind Key")]
    public sealed class RebindKeySO : ScriptableObject
    {
        public int Index => _index;
        public string ActionName => _inputActionRef.action.name;

        [Header("Asset Ref")]
        [SerializeField] private InputActionReference _inputActionRef;

        [Header("Property")]
        [SerializeField] private int _index;
    }
}
