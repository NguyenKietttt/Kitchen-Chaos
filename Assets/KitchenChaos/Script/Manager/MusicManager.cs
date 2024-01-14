using UnityEngine;

namespace KitchenChaos
{
    public sealed class MusicManager : MonoBehaviour
    {
        private const string MUSIC_VOLUMN_KEY = "MUSIC_VOLUMN_KEY";

        private const float MAX_VOLUMN = 1.0f;
        private const float DEFAULT_VOLUMN = 0.5f;

        public float MasterVolumn => _masterVolumn;

        [Header("Internal Ref")]
        [SerializeField] private AudioSource _audioSrc;

        private float _masterVolumn = DEFAULT_VOLUMN;

        private void Start()
        {
            _masterVolumn = PlayerPrefs.GetFloat(MUSIC_VOLUMN_KEY, DEFAULT_VOLUMN);
            _audioSrc.volume = _masterVolumn;
        }

        public void ChangeVolumn()
        {
            _masterVolumn += 0.1f;

            if (_masterVolumn > MAX_VOLUMN)
            {
                _masterVolumn = 0;
            }

            _audioSrc.volume = _masterVolumn;

            PlayerPrefs.SetFloat(MUSIC_VOLUMN_KEY, _masterVolumn);
            PlayerPrefs.Save();
        }
    }
}
