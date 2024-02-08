using UnityEngine;

[CreateAssetMenu(fileName = "Cfg_PlateCounter_Visual", menuName = "Scriptable Object/Config/Counter/PlateCounterVisual")]
public sealed class PlateCounterVisualCfg : ScriptableObject
{
    public GameObject PlateVisualPrefab => _plateVisualPrefab;

    public float PlateOffsetY => _plateOffsetY;
    public int PlateAmountMin => _plateAmountMin;

    [Header("Asset Ref")]
    [SerializeField] private GameObject _plateVisualPrefab;

    [Header("Property")]
    [SerializeField] private float _plateOffsetY;
    [SerializeField] private int _plateAmountMin;
}
