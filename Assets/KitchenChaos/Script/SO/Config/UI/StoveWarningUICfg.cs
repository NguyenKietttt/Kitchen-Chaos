using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_StoveWarningUI", menuName = "Scriptable Object/Config/UI/Stove Warning UI")]
    public sealed class StoveWarningUICfg : ScriptableObject
    {
        public float BurnProgressAmount => _burnProgressAmount;

        [Header("Property")]
        [SerializeField] private float _burnProgressAmount;
    }
}
