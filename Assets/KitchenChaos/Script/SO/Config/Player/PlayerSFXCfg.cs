using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_PlayerSFX", menuName = "Scriptable Object/Config/Player/PlayerSFX")]
    public sealed class PlayerSFXCfg : ScriptableObject
    {
        public float FootstepTimerMin => _footstepTimerMin;
        public float FootstepTimerMax => _footstepTimerMax;

        [Header("Property")]
        [SerializeField] private float _footstepTimerMin;
        [SerializeField] private float _footstepTimerMax;
    }
}
