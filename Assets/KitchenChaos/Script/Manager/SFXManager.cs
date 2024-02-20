using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class SFXManager : MonoBehaviour
    {
        public float MasterVolume => _masterVolume;

        [Header("Config")]
        [SerializeField] private SFXManagerCfg _config;

        private float _masterVolume;

        public void Init()
        {
            Bootstrap.Instance.EventMgr.DeliverReceiptSuccess += OnDeliverReceiptSuccess;
            Bootstrap.Instance.EventMgr.DeliverReceiptFailed += OnDeliverReceiptFailed;
            Bootstrap.Instance.EventMgr.CountdownPopup += OnCountdownPopup;
            Bootstrap.Instance.EventMgr.StoveWarning += OnStoveWarning;
            Bootstrap.Instance.EventMgr.InteractWithCutCounter += OnInteractWithCutCounter;
            Bootstrap.Instance.EventMgr.PickSomething += OnPickSomething;
            Bootstrap.Instance.EventMgr.PlaceObject += OnObjectPlaced;
            Bootstrap.Instance.EventMgr.InteractWithTrashCounter += OnInteractWithTrashCounter;

            _masterVolume = PlayerPrefs.GetFloat(_config.PlayerPrefsVolumeKey, _config.DefaultVolume);
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.DeliverReceiptSuccess -= OnDeliverReceiptSuccess;
            Bootstrap.Instance.EventMgr.DeliverReceiptFailed -= OnDeliverReceiptFailed;
            Bootstrap.Instance.EventMgr.CountdownPopup -= OnCountdownPopup;
            Bootstrap.Instance.EventMgr.StoveWarning -= OnStoveWarning;
            Bootstrap.Instance.EventMgr.InteractWithCutCounter -= OnInteractWithCutCounter;
            Bootstrap.Instance.EventMgr.PickSomething -= OnPickSomething;
            Bootstrap.Instance.EventMgr.PlaceObject -= OnObjectPlaced;
            Bootstrap.Instance.EventMgr.InteractWithTrashCounter -= OnInteractWithTrashCounter;
        }

        public void ChangeVolume()
        {
            _masterVolume += _config.VolumeStep;

            if (_masterVolume > _config.VolumeMax)
            {
                _masterVolume = _config.VolumeMin;
            }

            PlayerPrefs.SetFloat(_config.PlayerPrefsVolumeKey, _masterVolume);
            PlayerPrefs.Save();
        }

        public AudioClip GetRandomFootStepAudioClip()
        {
            return _config.FootstepClips[Random.Range(0, _config.FootstepClips.Count)];
        }

        private void OnCountdownPopup()
        {
            PlaySound(_config.WarningClips, Camera.main.transform.position, _config.CountdownPopupVolume);
        }

        private void OnStoveWarning()
        {
            PlaySound(_config.WarningClips, Camera.main.transform.position, _config.CountdownPopupVolume);
        }

        private void OnInteractWithCutCounter()
        {
            PlaySound(_config.ChopClips, Camera.main.transform.position, _config.ChopVolume);
        }

        private void OnPickSomething()
        {
            PlaySound(_config.ObjectPickupClips, Camera.main.transform.position, _config.PickupVolume);
        }

        private void OnObjectPlaced()
        {
            PlaySound(_config.ObjectDropClips, Camera.main.transform.position, _config.DropVolume);
        }

        private void OnInteractWithTrashCounter()
        {
            PlaySound(_config.TrashClips, Camera.main.transform.position, _config.TrashVolume);
        }

        private void OnDeliverReceiptSuccess()
        {
            PlaySound(_config.DeliverySuccessClips, Camera.main.transform.position, _config.DeliverySuccessVolume);
        }

        private void OnDeliverReceiptFailed()
        {
            PlaySound(_config.DeliveryFailClips, Camera.main.transform.position, _config.DeliveryFailedVolume);
        }

        private void PlaySound(IReadOnlyList<AudioClip> audioClips, Vector3 position, float volumeMultiplier = 1)
        {
            int index = Random.Range(0, audioClips.Count);
            float finalVolumn = volumeMultiplier * _masterVolume;
            AudioSource.PlayClipAtPoint(audioClips[index], position, finalVolumn);
        }
    }
}
