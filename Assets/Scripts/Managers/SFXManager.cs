using UnityEngine;
using Victor.Tools;

public sealed class SFXManager : MonoBehaviour
{
    [Header("Asset Ref")]
    [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

    [Header("Property")]
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _chopVolume;
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _pickupVolume;
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _dropVolume;
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _trashVolume;
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _deliverySuccessVolume;
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _deliveryFailedVolume;

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.DeliverReceiptSuccess -= OnDeliverReceiptSuccess;
        Bootstrap.Instance.EventMgr.DeliverReceiptFailed -= OnDeliverReceiptFailed;
        CuttingCounter.CutObject -= OnCut;
        PlayerController.PickSomething -= OnPickSomething;
        BaseCounter.ObjectPlaced -= OnObjectPlaced;
        TrashCounter.ObjectTrashed -= OnObjectTrashed;
    }

    public void Init()
    {
        Bootstrap.Instance.EventMgr.DeliverReceiptSuccess += OnDeliverReceiptSuccess;
        Bootstrap.Instance.EventMgr.DeliverReceiptFailed += OnDeliverReceiptFailed;
        CuttingCounter.CutObject += OnCut;
        PlayerController.PickSomething += OnPickSomething;
        BaseCounter.ObjectPlaced += OnObjectPlaced;
        TrashCounter.ObjectTrashed += OnObjectTrashed;
    }

    public AudioClip GetRandomFootStepAudioClip()
    {
        return _audioClipRefsSO.Footstep[Random.Range(0, _audioClipRefsSO.Footstep.Length)];
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

    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1)
    {
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volume);
    }
}
