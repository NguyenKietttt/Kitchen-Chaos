using System;
using UnityEngine;

namespace KitchenChaos
{
    [Serializable]
    public sealed class KitchenObjSOToGameObj
    {
        public KitchenObjectSO KitchenObjSO => _kitchenObjSO;
        public GameObject GameObj => _gameObj;

        [SerializeField] private KitchenObjectSO _kitchenObjSO;
        [SerializeField] private GameObject _gameObj;
    }
}
