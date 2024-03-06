using System.Collections.Generic;
using KitchenChaos.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class DeliveryManagerSingleUI : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private DeliveryManagerSingleUICfg? _config;

        [Header("Internal Ref")]
        [SerializeField] private TextMeshProUGUI? _receiptNameTxt;
        [SerializeField] private Transform? _ingredientContainer;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void Awake()
        {
            _config!.IngredientImg.gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetReceiptName(string name)
        {
            _receiptNameTxt!.SetText(name);
        }

        public void SetIngredientIcons(IReadOnlyList<KitchenObjectSO> KitchenObjsSO)
        {
            ClearPreviousIngredientICons();

            foreach (KitchenObjectSO kitchenObjSO in KitchenObjsSO)
            {
                Image ingredientIcon = Instantiate(_config!.IngredientImg, _ingredientContainer);
                ingredientIcon.sprite = kitchenObjSO.Sprite;
                ingredientIcon.gameObject.SetActive(true);
            }
        }

        private void ClearPreviousIngredientICons()
        {
            foreach (Transform child in _ingredientContainer!)
            {
                Destroy(child.gameObject);
            }
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null || _receiptNameTxt == null || _ingredientContainer == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }
    }
}
