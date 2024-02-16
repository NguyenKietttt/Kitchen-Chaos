using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_StoveCounter_SFX", menuName = "Scriptable Object/Config/Counter/StoveCounterSFX")]
    public sealed class StoveCounterSFXCfg : ScriptableObject
    {
        public float BurnProgressAmount => _burnProgressAmount;
        public float WarningTimerMin => _warningTimerMin;
        public float WarningTimerMax => _warningTimerMax;

        [Header("Property")]
        [SerializeField] private float _burnProgressAmount;
        [SerializeField] private float _warningTimerMin;
        [SerializeField] private float _warningTimerMax;
    }
}
