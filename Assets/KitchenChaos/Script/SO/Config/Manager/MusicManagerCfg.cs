using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_MusicManager", menuName = "Scriptable Object/Config/Manager/MusicManager")]
    public sealed class MusicManagerCfg : ScriptableObject
    {
        public string PlayerPrefsVolumnKey => _playerPrefsVolumnKey;

        public float VolumnMin => _volumnMin;
        public float VolumnMax => _volumnMax;
        public float DefaultVolumn => _defaultVolumn;
        public float VolumnStep => _volumnStep;

        [Header("Property")]
        [SerializeField] private string _playerPrefsVolumnKey;

        [Space]

        [SerializeField] private float _volumnMin;
        [SerializeField] private float _volumnMax;
        [SerializeField] private float _defaultVolumn;
        [SerializeField] private float _volumnStep;
    }
}
