using UnityEngine;
using Victor.Tools;

namespace KitchenChaos
{
    public sealed class SFXManager : MonoBehaviour
    {
        private const string SFX_VOLUMN_KEY = "SFX_VOLUMN_KEY";
        private const float MAX_VOLUMN = 1.0f;

        public float MasterVolumn => _masterVolumn;

        [Header("Asset Ref")]
        [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

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
            CuttingCounter.CutObject += OnCut;
            PlayerController.PickSomething += OnPickSomething;
            BaseCounter.ObjectPlaced += OnObjectPlaced;
            TrashCounter.ObjectTrashed += OnObjectTrashed;

            _masterVolumn = PlayerPrefs.GetFloat(SFX_VOLUMN_KEY, MAX_VOLUMN);
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.DeliverReceiptSuccess -= OnDeliverReceiptSuccess;
            Bootstrap.Instance.EventMgr.DeliverReceiptFailed -= OnDeliverReceiptFailed;
            Bootstrap.Instance.EventMgr.CountdownPopup -= OnCoundownPopup;
            Bootstrap.Instance.EventMgr.StoveWarning -= OnStoveWarning;
            CuttingCounter.CutObject -= OnCut;
            PlayerController.PickSomething -= OnPickSomething;
            BaseCounter.ObjectPlaced -= OnObjectPlaced;
            TrashCounter.ObjectTrashed -= OnObjectTrashed;
        }

        public void ChangeVolumn()
        {
            _masterVolumn += 0.1f;

            if (_masterVolumn > MAX_VOLUMN)
            {
                _masterVolumn = 0;
            }

            PlayerPrefs.SetFloat(SFX_VOLUMN_KEY, _masterVolumn);
            PlayerPrefs.Save();
        }

        public AudioClip GetRandomFootStepAudioClip()
        {
            return _audioClipRefsSO.Footstep[Random.Range(0, _audioClipRefsSO.Footstep.Length)];
        }

        private void OnCoundownPopup()
        {
            PlaySound(_audioClipRefsSO.Warning, Camera.main.transform.position, _countdownPopupVolume);
        }

        private void OnStoveWarning()
        {
            PlaySound(_audioClipRefsSO.Warning, Camera.main.transform.position, _countdownPopupVolume);
        }

        private void OnCut()
        {
            PlaySound(_audioClipRefsSO.Chop, Camera.main.transform.position, _chopVolume);
        }

        private void OnPickSomething()
        {
            PlaySound(_audioClipRefsSO.ObjectPickup, Camera.main.transform.position, _pickupVolume);
        }

        private void OnObjectPlaced()
        {
            PlaySound(_audioClipRefsSO.ObjectDrop, Camera.main.transform.position, _dropVolume);
        }

        private void OnObjectTrashed()
        {
            PlaySound(_audioClipRefsSO.Trash, Camera.main.transform.position, _trashVolume);
        }

        private void OnDeliverReceiptSuccess()
        {
            PlaySound(_audioClipRefsSO.DeliverySuccess, Camera.main.transform.position, _deliverySuccessVolume);
        }

        private void OnDeliverReceiptFailed()
        {
            PlaySound(_audioClipRefsSO.DeliveryFail, Camera.main.transform.position, _deliveryFailedVolume);
        }

        private void PlaySound(AudioClip[] audioClips, Vector3 position, float volumeMultiplier = 1)
        {
            AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volumeMultiplier * _masterVolumn);
        }
    }
}
