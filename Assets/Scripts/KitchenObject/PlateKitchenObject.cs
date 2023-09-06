using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class PlateKitchenObject : KitchenObject
{
    [Header("Child SO")]
    [SerializeField] private KitchenObjectSO[] _validListKitchenObjSO;

    private readonly HashSet<KitchenObjectSO> _listKitchenObjSO = new();

    public bool TryAddIngredient(KitchenObjectSO kitchenObjSO)
    {
        if (!_validListKitchenObjSO.Contains(kitchenObjSO))
        {
            return false;
        }

        return _listKitchenObjSO.Add(kitchenObjSO);
    }
}
