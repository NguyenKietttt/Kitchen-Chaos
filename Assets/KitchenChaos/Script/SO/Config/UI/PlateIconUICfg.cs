using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_PlateIconUI", menuName = "Scriptable Object/Config/UI/Plate Icon UI")]
    public sealed class PlateIconUICfg : ScriptableObject
    {
        public PlateIconSingleUI PlateIconSingleUI => _plateIconSingleUI!;

        [Header("Asset Ref")]
        [SerializeField] private PlateIconSingleUI? _plateIconSingleUI;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void CheckNullEditorReferences()
        {
            if (_plateIconSingleUI == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }
    }
}
