using UnityEngine;

[CreateAssetMenu(fileName = "Cfg_ProgressBarFlashingUI", menuName = "Scriptable Object/Config/UI/Progress Bar Flashing UI")]
public sealed class ProgressBarFlashingUICfg : ScriptableObject
{
    public Color IdleColor => _idleColor;
    public Color WarningColor => _warningColor;
    public float BurnProgressAmount => _burnProgressAmount;

    [Header("Property")]
    [SerializeField] private Color _idleColor;
    [SerializeField] private Color _warningColor;

    [Space]

    [SerializeField] private float _burnProgressAmount;
}
