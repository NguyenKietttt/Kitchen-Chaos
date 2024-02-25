using UnityEngine;

namespace KitchenChaos
{
    public sealed class MusicManager : MonoBehaviour
    {
        public float MasterVolume => _masterVolume;

        [Header("Config")]
        [SerializeField] private MusicManagerCfg _config;

        [Header("Internal Ref")]
        [SerializeField] private AudioSource _audioSrc;

        private float _masterVolume;

        public void Init()
        {
            _masterVolume = PlayerPrefs.GetFloat(_config.PlayerPrefsVolumeKey, _config.DefaultVolume);
            _audioSrc.volume = _masterVolume;
        }

        public void ChangeVolume()
        {
            _masterVolume += _config.VolumeStep;

            if (_masterVolume > _config.VolumeMax)
            {
                _masterVolume = _config.VolumeMin;
            }

            _audioSrc.volume = _masterVolume;

            PlayerPrefs.SetFloat(_config.PlayerPrefsVolumeKey, _masterVolume);
            PlayerPrefs.Save();
        }
    }
}
