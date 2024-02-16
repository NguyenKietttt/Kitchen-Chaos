using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_ProgressBarUI", menuName = "Scriptable Object/Config/UI/Progress Bar UI")]
    public sealed class ProgressBarUICfg : ScriptableObject
    {
        public int MinProgress => _minProgress;
        public int MaxProgress => _maxProgress;

        [SerializeField] private int _minProgress;
        [SerializeField] private int _maxProgress;
    }
}
