using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_KitchenObject", menuName = "Scriptable Object/Config/Kitchen Object/Kitchen Object")]
    public sealed class KitchenObjectCfg : ScriptableObject
    {
        public KitchenObjectSO KitchenObjSO => _kitchenObjSO;

        [Header("Asset Ref")]
        [SerializeField] private KitchenObjectSO _kitchenObjSO;
    }
}
