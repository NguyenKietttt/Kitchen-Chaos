using KitchenChaos.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class PlateIconSingleUI : MonoBehaviour
    {
        [Header("Internal Ref")]
        [SerializeField] private Image? _iconImg;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        public void SetIcon(KitchenObjectSO kitchenObjectSO)
        {
            _iconImg!.sprite = kitchenObjectSO.Sprite;
        }

        private void CheckNullEditorReferences()
        {
            if (_iconImg == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }
    }
}
