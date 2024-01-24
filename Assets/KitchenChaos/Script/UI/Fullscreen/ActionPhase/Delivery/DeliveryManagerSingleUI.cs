using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class DeliveryManagerSingleUI : MonoBehaviour
    {
        [Header("Asset Ref")]
        [SerializeField] private Image _ingredientImg;

        [Header("Internal Ref")]
        [SerializeField] private TextMeshProUGUI _receiptNameTxt;
        [SerializeField] private Transform _ingredientContainer;

        private void Awake()
        {
            _ingredientImg.gameObject.SetActive(false);
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
            _receiptNameTxt.SetText(name);
        }

        public void SetIngredientIcons(IReadOnlyList<KitchenObjectSO> KitchenObjsSO)
        {
            ClearPreviousIngredientICons();

            foreach (KitchenObjectSO kitchenObjSO in KitchenObjsSO)
            {
                Image ingredientIcon = Instantiate(_ingredientImg, _ingredientContainer);
                ingredientIcon.sprite = kitchenObjSO.Sprite;
                ingredientIcon.gameObject.SetActive(true);
            }
        }

        private void ClearPreviousIngredientICons()
        {
            foreach (Transform child in _ingredientContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
