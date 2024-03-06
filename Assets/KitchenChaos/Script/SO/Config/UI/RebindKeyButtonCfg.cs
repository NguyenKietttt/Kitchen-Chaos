using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_RebindKeyButton", menuName = "Scriptable Object/Config/UI/Rebind Key Button")]
    public sealed class RebindKeyButtonCfg : ScriptableObject
    {
        public RebindKeySO RebindKeySO => _rebindKeySO!;

        [Header("Asset Ref")]
        [SerializeField] private RebindKeySO? _rebindKeySO;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void CheckNullEditorReferences()
        {
            if (_rebindKeySO == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }
    }
}
