using UnityEngine;
using Victor.Tools;

public sealed class SFXManager : MonoBehaviour
{
    [Header("Asset Ref")]
    [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

    [Header("Property")]
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _chopVolumn;
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _pickupVolumn;
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _dropVolumn;
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _trashVolumn;
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _deliverySuccessVolumn;
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _deliveryFailedVolumn;

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
        PlaySound(_audioClipRefsSO.Chop, Camera.main.transform.position, _chopVolumn);
    }

    private void OnPickSomething()
    {
        PlaySound(_audioClipRefsSO.ObjectPickup, Camera.main.transform.position, _pickupVolumn);
    }

    private void OnObjectPlaced()
    {
        PlaySound(_audioClipRefsSO.ObjectDrop, Camera.main.transform.position, _dropVolumn);
    }

    private void OnObjectTrashed()
    {
        PlaySound(_audioClipRefsSO.Trash, Camera.main.transform.position, _trashVolumn);
    }

    private void OnDeliverReceiptSuccess()
    {
        PlaySound(_audioClipRefsSO.DeliverySuccess, Camera.main.transform.position, _deliverySuccessVolumn);
    }

    private void OnDeliverReceiptFailed()
    {
        PlaySound(_audioClipRefsSO.DeliveryFail, Camera.main.transform.position, _deliveryFailedVolumn);
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volumn = 1)
    {
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volumn);
    }
}
