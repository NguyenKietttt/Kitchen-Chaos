using System.Collections.Generic;
using KitchenChaos.Utils;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class SFXManager : MonoBehaviour
    {
        public float MasterVolume => _masterVolume;

        [Header("Config")]
        [SerializeField] private SFXManagerCfg? _config;

        private EventManager? _eventMgr;

        private float _masterVolume;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        public void Init()
        {
            RegisterServices();
            SubscribeEvents();

            _masterVolume = PlayerPrefs.GetFloat(_config!.PlayerPrefsVolumeKey, _config.DefaultVolume);
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        public void ChangeVolume()
        {
            _masterVolume += _config!.VolumeStep;

            if (_masterVolume > _config.VolumeMax)
            {
                _masterVolume = _config.VolumeMin;
            }

            PlayerPrefs.SetFloat(_config.PlayerPrefsVolumeKey, _masterVolume);
            PlayerPrefs.Save();
        }

        public AudioClip GetRandomFootStepAudioClip()
        {
            return _config!.FootstepClips[Random.Range(0, _config.FootstepClips.Count)];
        }

        private void OnCountdownPopup()
        {
            PlaySound(_config!.WarningClips, Camera.main.transform.position, _config.CountdownPopupVolume);
        }

        private void OnStoveWarning()
        {
            PlaySound(_config!.WarningClips, Camera.main.transform.position, _config.CountdownPopupVolume);
        }

        private void OnInteractWithCutCounter()
        {
            PlaySound(_config!.ChopClips, Camera.main.transform.position, _config.ChopVolume);
        }

        private void OnPickSomething()
        {
            PlaySound(_config!.ObjectPickupClips, Camera.main.transform.position, _config.PickupVolume);
        }

        private void OnObjectPlaced()
        {
            PlaySound(_config!.ObjectDropClips, Camera.main.transform.position, _config.DropVolume);
        }

        private void OnInteractWithTrashCounter()
        {
            PlaySound(_config!.TrashClips, Camera.main.transform.position, _config.TrashVolume);
        }

        private void OnDeliverReceiptSuccess()
        {
            PlaySound(_config!.DeliverySuccessClips, Camera.main.transform.position, _config.DeliverySuccessVolume);
        }

        private void OnDeliverReceiptFailed()
        {
            PlaySound(_config!.DeliveryFailClips, Camera.main.transform.position, _config.DeliveryFailedVolume);
        }

        private void PlaySound(IReadOnlyList<AudioClip> audioClips, Vector3 position, float volumeMultiplier = 1)
        {
            int index = Random.Range(0, audioClips.Count);
            float finalVolume = volumeMultiplier * _masterVolume;
            AudioSource.PlayClipAtPoint(audioClips[index], position, finalVolume);
        }

        private void CheckNullEditorReferences()
        {
            if (_config == null)
            {
                CustomLog.LogError(this, "missing internal references in editor!");
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
        }

        private void SubscribeEvents()
        {
            _eventMgr!.DeliverReceiptSuccess += OnDeliverReceiptSuccess;
            _eventMgr!.DeliverReceiptFailed += OnDeliverReceiptFailed;
            _eventMgr!.CountdownPopup += OnCountdownPopup;
            _eventMgr!.StoveWarning += OnStoveWarning;
            _eventMgr!.InteractWithCutCounter += OnInteractWithCutCounter;
            _eventMgr!.PickSomething += OnPickSomething;
            _eventMgr!.PlaceObject += OnObjectPlaced;
            _eventMgr!.InteractWithTrashCounter += OnInteractWithTrashCounter;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr!.DeliverReceiptSuccess -= OnDeliverReceiptSuccess;
            _eventMgr!.DeliverReceiptFailed -= OnDeliverReceiptFailed;
            _eventMgr!.CountdownPopup -= OnCountdownPopup;
            _eventMgr!.StoveWarning -= OnStoveWarning;
            _eventMgr!.InteractWithCutCounter -= OnInteractWithCutCounter;
            _eventMgr!.PickSomething -= OnPickSomething;
            _eventMgr!.PlaceObject -= OnObjectPlaced;
            _eventMgr!.InteractWithTrashCounter -= OnInteractWithTrashCounter;
        }
    }
}
