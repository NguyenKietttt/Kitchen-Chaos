using System.Collections.Generic;
using UnityEngine;
using Victor.Tools;

namespace KitchenChaos
{
    public sealed class SFXManager : MonoBehaviour
    {
        private const string SFX_VOLUMN_KEY = "SFX_VOLUMN_KEY";
        private const float MAX_VOLUMN = 1.0f;
        private const float MIN_VOLUMN = 0.0f;
        private const float VOLUME_STEP = 0.1f;

        public float MasterVolumn => _masterVolumn;

        [Header("Asset Ref")]
        [SerializeField] private SFXClipsSO _sfxClipsSO;

        [Header("Property")]
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _chopVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _pickupVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _dropVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _trashVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _deliverySuccessVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _deliveryFailedVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _countdownPopupVolume;

        private float _masterVolumn = MAX_VOLUMN;

        public void Init()
        {
            Bootstrap.Instance.EventMgr.DeliverReceiptSuccess += OnDeliverReceiptSuccess;
            Bootstrap.Instance.EventMgr.DeliverReceiptFailed += OnDeliverReceiptFailed;
            Bootstrap.Instance.EventMgr.CountdownPopup += OnCoundownPopup;
            Bootstrap.Instance.EventMgr.StoveWarning += OnStoveWarning;
            Bootstrap.Instance.EventMgr.InteractWithCutCounter += OnInteractWithCutCounter;
            Bootstrap.Instance.EventMgr.PickSomething += OnPickSomething;
            Bootstrap.Instance.EventMgr.PlaceObject += OnObjectPlaced;
            Bootstrap.Instance.EventMgr.InteractWithTrashCounter += OnInteractWithTrashCounter;

            _masterVolumn = PlayerPrefs.GetFloat(SFX_VOLUMN_KEY, MAX_VOLUMN);
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.DeliverReceiptSuccess -= OnDeliverReceiptSuccess;
            Bootstrap.Instance.EventMgr.DeliverReceiptFailed -= OnDeliverReceiptFailed;
            Bootstrap.Instance.EventMgr.CountdownPopup -= OnCoundownPopup;
            Bootstrap.Instance.EventMgr.StoveWarning -= OnStoveWarning;
            Bootstrap.Instance.EventMgr.InteractWithCutCounter -= OnInteractWithCutCounter;
            Bootstrap.Instance.EventMgr.PickSomething -= OnPickSomething;
            Bootstrap.Instance.EventMgr.PlaceObject -= OnObjectPlaced;
            Bootstrap.Instance.EventMgr.InteractWithTrashCounter -= OnInteractWithTrashCounter;
        }

        public void ChangeVolumn()
        {
            _masterVolumn += VOLUME_STEP;

            if (_masterVolumn > MAX_VOLUMN)
            {
                _masterVolumn = MIN_VOLUMN;
            }

            PlayerPrefs.SetFloat(SFX_VOLUMN_KEY, _masterVolumn);
            PlayerPrefs.Save();
        }

        public AudioClip GetRandomFootStepAudioClip()
        {
            return _sfxClipsSO.Footstep[Random.Range(0, _sfxClipsSO.Footstep.Count)];
        }

        private void OnCoundownPopup()
        {
            PlaySound(_sfxClipsSO.Warning, Camera.main.transform.position, _countdownPopupVolume);
        }

        private void OnStoveWarning()
        {
            PlaySound(_sfxClipsSO.Warning, Camera.main.transform.position, _countdownPopupVolume);
        }

        private void OnInteractWithCutCounter()
        {
            PlaySound(_sfxClipsSO.Chop, Camera.main.transform.position, _chopVolume);
        }

        private void OnPickSomething()
        {
            PlaySound(_sfxClipsSO.ObjectPickup, Camera.main.transform.position, _pickupVolume);
        }

        private void OnObjectPlaced()
        {
            PlaySound(_sfxClipsSO.ObjectDrop, Camera.main.transform.position, _dropVolume);
        }

        private void OnInteractWithTrashCounter()
        {
            PlaySound(_sfxClipsSO.Trash, Camera.main.transform.position, _trashVolume);
        }

        private void OnDeliverReceiptSuccess()
        {
            PlaySound(_sfxClipsSO.DeliverySuccess, Camera.main.transform.position, _deliverySuccessVolume);
        }

        private void OnDeliverReceiptFailed()
        {
            PlaySound(_sfxClipsSO.DeliveryFail, Camera.main.transform.position, _deliveryFailedVolume);
        }

        private void PlaySound(IReadOnlyList<AudioClip> audioClips, Vector3 position, float volumeMultiplier = 1)
        {
            int index = Random.Range(0, audioClips.Count);
            float finalVolumn = volumeMultiplier * _masterVolumn;
            AudioSource.PlayClipAtPoint(audioClips[index], position, finalVolumn);
        }
    }
}
