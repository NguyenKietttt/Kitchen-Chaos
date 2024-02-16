using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_DeliveryResultUI", menuName = "Scriptable Object/Config/UI/Delivery Result UI")]
    public sealed class DeliveryResultUICfg : ScriptableObject
    {
        public Sprite SuccessSpr => _successSpr;
        public Sprite FailedSpr => _failedSpr;

        public Color SuccessColor => _successColor;
        public Color FailedColor => _failedColor;

        [Header("Asset Ref")]
        [SerializeField] private Sprite _successSpr;
        [SerializeField] private Sprite _failedSpr;

        [Header("Property")]
        [SerializeField] private Color _successColor;
        [SerializeField] private Color _failedColor;
    }
}
