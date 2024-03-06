using KitchenChaos.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_DeliveryManagerSingleUI", menuName = "Scriptable Object/Config/UI/Delivery Manager Single UI")]
    public sealed class DeliveryManagerSingleUICfg : ScriptableObject
    {
        public Image IngredientImg => _ingredientImg!;

        [Header("Asset Ref")]
        [SerializeField] private Image? _ingredientImg;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void CheckNullEditorReferences()
        {
            if (_ingredientImg == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }
    }
}
