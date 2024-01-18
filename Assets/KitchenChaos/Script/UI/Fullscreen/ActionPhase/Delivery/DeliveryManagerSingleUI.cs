using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class DeliveryManagerSingleUI : MonoBehaviour
    {
        [Header("Asset Ref")]
        [SerializeField] private Image _ingredientImage;

        [Header("Internal Ref")]
        [SerializeField] private TextMeshProUGUI _receiptNameText;
        [SerializeField] private Transform _ingredientContainer;

        private void Awake()
        {
            _ingredientImage.gameObject.SetActive(false);
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
            _receiptNameText.SetText(name);
        }

        public void SetIngredientIcons(KitchenObjectSO[] listKitchenObjSO)
        {
            ClearPreviousIngredientICons();

            foreach (KitchenObjectSO kitchenObjSO in listKitchenObjSO)
            {
                Image ingredientIcon = Instantiate(_ingredientImage, _ingredientContainer);
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
