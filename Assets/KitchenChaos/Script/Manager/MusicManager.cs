using UnityEngine;

namespace KitchenChaos
{
    public sealed class MusicManager : MonoBehaviour
    {
        public float MasterVolumn => _masterVolumn;

        [Header("Config")]
        [SerializeField] private MusicManagerCfg _config;

        [Header("Internal Ref")]
        [SerializeField] private AudioSource _audioSrc;

        private float _masterVolumn;

        private void Start()
        {
            _masterVolumn = PlayerPrefs.GetFloat(_config.PlayerPrefsVolumnKey, _config.DefaultVolumn);
            _audioSrc.volume = _masterVolumn;
        }

        public void ChangeVolumn()
        {
            _masterVolumn += _config.VolumnStep;

            if (_masterVolumn > _config.VolumnMax)
            {
                _masterVolumn = _config.VolumnMin;
            }

            _audioSrc.volume = _masterVolumn;

            PlayerPrefs.SetFloat(_config.PlayerPrefsVolumnKey, _masterVolumn);
            PlayerPrefs.Save();
        }
    }
}
