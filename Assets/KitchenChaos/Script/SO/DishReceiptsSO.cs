using System;
using System.Collections.Generic;
using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "SO_DishReceiptsSO", menuName = "Scriptable Object/Dish Receipts")]
    public sealed class DishReceiptsSO : ScriptableObject
    {
        public IReadOnlyList<DishReceiptSO> Receipts => Array.AsReadOnly(_receipts)!;

        [Header("Asset Ref")]
        [SerializeField] private DishReceiptSO[]? _receipts;

        private void OnValidate()
        {
            if (_receipts?.Length <= 0)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }
    }
}
