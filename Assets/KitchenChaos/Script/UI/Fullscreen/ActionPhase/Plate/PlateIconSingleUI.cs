using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class PlateIconSingleUI : MonoBehaviour
    {
        [Header("Internal Ref")]
        [SerializeField] private Image _iconImg;

        public void SetIcon(KitchenObjectSO kitchenObjectSO)
        {
            _iconImg.sprite = kitchenObjectSO.Sprite;
        }
    }
}
