using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_StoveCounter", menuName = "Scriptable Object/Config/Counter/StoveCounter")]
    public sealed class StoveCounterCfg : ScriptableObject
    {
        public IReadOnlyList<FryingReceiptSO> FryingReceipts => Array.AsReadOnly(_fryingReceipts);
        public IReadOnlyList<BurningReceiptSO> BurningReceipts => Array.AsReadOnly(_burningReceipts);

        public float BurningTimerMin => _burningTimerMin;
        public float FryingTimerMin => _fryingTimerMin;
        public float ProgressMin => _progressMin;

        [Header("Asset Ref")]
        [SerializeField] private FryingReceiptSO[] _fryingReceipts;
        [SerializeField] private BurningReceiptSO[] _burningReceipts;

        [Header("Property")]
        [SerializeField] private float _burningTimerMin;
        [SerializeField] private float _fryingTimerMin;
        [SerializeField] private float _progressMin;
    }
}
