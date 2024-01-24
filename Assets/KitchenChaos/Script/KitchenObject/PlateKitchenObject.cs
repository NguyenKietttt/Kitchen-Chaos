using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlateKitchenObject : KitchenObject
    {
        public IReadOnlyCollection<KitchenObjectSO> KitchenObjHashSet => _kitchenObjHashSet;

        [Header("Child Asset Ref")]
        [SerializeField] private KitchenObjectSO[] _validKitchenObjs;

        private readonly HashSet<KitchenObjectSO> _kitchenObjHashSet = new();

        public bool TryAddIngredient(KitchenObjectSO kitchenObjSO)
        {
            if (!_validKitchenObjs.Contains(kitchenObjSO))
            {
                return false;
            }

            if (_kitchenObjHashSet.Add(kitchenObjSO))
            {
                Bootstrap.Instance.EventMgr.AddIngredientSuccess?.Invoke(GetInstanceID(), kitchenObjSO);
                return true;
            }

            return false;
        }
    }
}
