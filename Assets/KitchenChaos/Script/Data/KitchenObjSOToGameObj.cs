using System;
using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [Serializable]
    public sealed class KitchenObjSOToGameObj
    {
        [SerializeField] private KitchenObjectSO? _kitchenObjSO;
        [SerializeField] private GameObject? _gameObj;

        public KitchenObjectSO? GetKitchenObjSO()
        {
            if (_kitchenObjSO == null)
            {
                CustomLog.LogError(this, nameof(_kitchenObjSO), "missing references in editor!!!");
            }

            return _kitchenObjSO;
        }

        public GameObject? GetGameObj()
        {
            if (_gameObj == null)
            {
                CustomLog.LogError(this, nameof(_gameObj), "missing references in editor!!!");
            }

            return _gameObj;
        }
    }
}
