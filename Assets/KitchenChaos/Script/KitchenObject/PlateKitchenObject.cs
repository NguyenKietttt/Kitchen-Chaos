using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlateKitchenObject : KitchenObject
    {
        public IReadOnlyCollection<KitchenObjectSO> KitchenObjHashSet => _kitchenObjHashSet;

        private readonly HashSet<KitchenObjectSO> _kitchenObjHashSet = new();

        private PlateKitchenObjectCfg _plateConfig;

        private void Start()
        {
            _plateConfig = (PlateKitchenObjectCfg)_config;
        }

        public bool TryAddIngredient(KitchenObjectSO kitchenObjSO)
        {
            if (!_plateConfig.ValidKitchenObjSOs.Contains(kitchenObjSO))
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
