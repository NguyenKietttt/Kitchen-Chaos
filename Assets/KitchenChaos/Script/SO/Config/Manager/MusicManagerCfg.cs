using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_MusicManager", menuName = "Scriptable Object/Config/Manager/MusicManager")]
    public sealed class MusicManagerCfg : ScriptableObject
    {
        public string PlayerPrefsVolumeKey => _playerPrefsVolumeKey;

        public float VolumeMin => _volumeMin;
        public float VolumeMax => _volumeMax;
        public float DefaultVolume => _defaultVolume;
        public float VolumeStep => _volumeStep;

        [Header("Property")]
        [SerializeField] private string _playerPrefsVolumeKey = string.Empty;

        [Space]

        [SerializeField] private float _volumeMin;
        [SerializeField] private float _volumeMax;
        [SerializeField] private float _defaultVolume;
        [SerializeField] private float _volumeStep;
    }
}
