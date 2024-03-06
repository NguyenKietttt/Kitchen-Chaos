using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_PlateCounter_Visual", menuName = "Scriptable Object/Config/Counter/PlateCounterVisual")]
    public sealed class PlateCounterVisualCfg : ScriptableObject
    {
        public GameObject PlateVisualPrefab => _plateVisualPrefab!;

        public float PlateOffsetY => _plateOffsetY;
        public int PlateAmountMin => _plateAmountMin;

        [Header("Asset Ref")]
        [SerializeField] private GameObject? _plateVisualPrefab;

        [Header("Property")]
        [SerializeField] private float _plateOffsetY;
        [SerializeField] private int _plateAmountMin;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void CheckNullEditorReferences()
        {
            if (_plateVisualPrefab == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }
    }
}
