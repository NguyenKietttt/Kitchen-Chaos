using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "SO_DishReceiptsSO", menuName = "Scriptable Object/Dish Receipts")]
    public sealed class DishReceiptsSO : ScriptableObject
    {
        public IReadOnlyList<DishReceiptSO> Receipts => _receipts;

        [SerializeField] private DishReceiptSO[] _receipts;
    }
}
