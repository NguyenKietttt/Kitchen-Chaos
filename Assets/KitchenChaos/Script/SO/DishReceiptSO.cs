using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "SO_DishReceipt", menuName = "Scriptable Object/Dish Receipt")]
    public sealed class DishReceiptSO : ScriptableObject
    {
        public IReadOnlyList<KitchenObjectSO> KitchenObjsSO => Array.AsReadOnly(_kitchenObjsSO);
        public string Name => _name;

        [Header("Asset Ref")]
        [SerializeField] private KitchenObjectSO[] _kitchenObjsSO;

        [Header("Property")]
        [SerializeField] private string _name;
    }
}
