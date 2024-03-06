using System;
using System.Collections.Generic;
using System.Linq;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class PlateKitchenObject : KitchenObject
    {
        public IReadOnlyCollection<KitchenObjectSO> KitchenObjHashSet => _kitchenObjHashSet;

        private readonly HashSet<KitchenObjectSO> _kitchenObjHashSet = new();

        private EventManager? _eventMgr;
        private PlateKitchenObjectCfg? _plateConfig;

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            _plateConfig = (PlateKitchenObjectCfg)_config!;
        }

        private void OnDestroy()
        {
            DeregisterServices();
        }

        public bool TryAddIngredient(KitchenObjectSO kitchenObjSO)
        {
            if (!_plateConfig!.ValidKitchenObjSOs.Contains(kitchenObjSO))
            {
                return false;
            }

            if (_kitchenObjHashSet.Add(kitchenObjSO))
            {
                _eventMgr!.AddIngredientSuccess?.Invoke(GetInstanceID(), kitchenObjSO);
                return true;
            }

            return false;
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
        }
    }
}
