using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_KitchenObject_Plate", menuName = "Scriptable Object/Config/Kitchen Object/Plate")]
    public sealed class PlateKitchenObjectCfg : KitchenObjectCfg
    {
        public IReadOnlyList<KitchenObjectSO> ValidKitchenObjSOs => Array.AsReadOnly(_listValidKitchenObjSO);

        [Header("Child Asset Ref")]
        [SerializeField] private KitchenObjectSO[] _listValidKitchenObjSO;
    }
}
