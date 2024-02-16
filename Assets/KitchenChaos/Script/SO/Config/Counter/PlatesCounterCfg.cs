using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_PlateCounter", menuName = "Scriptable Object/Config/Counter/PlateCounter")]
    public sealed class PlatesCounterCfg : ScriptableObject
    {
        public KitchenObjectSO PlateSO => _plateSO;

        public float PlateSpawnTimerMin => _plateSpawnTimerMin;
        public float PlateSpawnTimerMax => _plateSpawnTimerMax;
        public int PlateSpawnAmountMin => _plateSpawnAmountMin;
        public int PlateSpawnAmountMax => _plateSpawnAmountMax;

        [Header("Asset Ref")]
        [SerializeField] private KitchenObjectSO _plateSO;

        [Header("Property")]
        [SerializeField] private float _plateSpawnTimerMin;
        [SerializeField] private float _plateSpawnTimerMax;
        [SerializeField] private int _plateSpawnAmountMin;
        [SerializeField] private int _plateSpawnAmountMax;
    }
}
