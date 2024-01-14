using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlateKitchenObject : KitchenObject
    {
        [Header("Child SO")]
        [SerializeField] private KitchenObjectSO[] _validListKitchenObjSO;

        private readonly HashSet<KitchenObjectSO> _listKitchenObjSO = new();

        public HashSet<KitchenObjectSO> GetListKitchenObjectSO()
        {
            return _listKitchenObjSO;
        }

        public bool TryAddIngredient(KitchenObjectSO kitchenObjSO)
        {
            if (!_validListKitchenObjSO.Contains(kitchenObjSO))
            {
                return false;
            }

            if (_listKitchenObjSO.Add(kitchenObjSO))
            {
                Bootstrap.Instance.EventMgr.AddIngredientSuccess?.Invoke(GetInstanceID(), kitchenObjSO);
                return true;
            }

            return false;
        }
    }
}
