using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_DeliveryManager", menuName = "Scriptable Object/Config/Manager/DeliveryManager")]
    public sealed class DeliveryManagerCfg : ScriptableObject
    {
        public DishReceiptsSO DishReceiptsS0 => _dishReceiptsS0!;

        public float SpawnReceiptTimerMin => _spawnReceiptTimerMin;
        public float SpawnReceiptTimerMax => _spawnReceiptTimerMax;
        public int WaitingReceiptMin => _waitingReceiptMin;
        public int WaitingReceiptMax => _waitingReceiptMax;

        [Header("Asset Ref")]
        [SerializeField] private DishReceiptsSO? _dishReceiptsS0;

        [Header("Property")]
        [SerializeField] private float _spawnReceiptTimerMin;
        [SerializeField] private float _spawnReceiptTimerMax;
        [SerializeField] private int _waitingReceiptMin;
        [SerializeField] private int _waitingReceiptMax;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void CheckNullEditorReferences()
        {
            if (_dishReceiptsS0 == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }
    }
}
