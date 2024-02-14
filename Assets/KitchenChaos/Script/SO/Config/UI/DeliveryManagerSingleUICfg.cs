using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Cfg_DeliveryManagerSingleUI", menuName = "Scriptable Object/Config/UI/Delivery Manager Single UI")]
public sealed class DeliveryManagerSingleUICfg : ScriptableObject
{
    public Image IngredientImg => _ingredientImg;

    [Header("Asset Ref")]
    [SerializeField] private Image _ingredientImg;
}
