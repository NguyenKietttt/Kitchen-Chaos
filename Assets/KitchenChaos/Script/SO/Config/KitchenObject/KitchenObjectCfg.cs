using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_KitchenObject", menuName = "Scriptable Object/Config/Kitchen Object/Kitchen Object")]
    public class KitchenObjectCfg : ScriptableObject
    {
        public KitchenObjectSO KitchenObjSO => _kitchenObjSO!;

        [Header("Asset Ref")]
        [SerializeField] private KitchenObjectSO? _kitchenObjSO;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void CheckNullEditorReferences()
        {
            if (_kitchenObjSO == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }
    }
}
