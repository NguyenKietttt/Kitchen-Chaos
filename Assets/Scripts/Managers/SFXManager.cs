using UnityEngine;
using Victor.Tools;

public sealed class SFXManager : MonoBehaviour
{
    [Header("Asset Ref")]
    [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

    [Header("Property")]
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _deliverySuccessVolumn;
    [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _deliveryFailedVolumn;

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.DeliverReceiptSuccess -= OnDeliverReceiptSuccess;
        Bootstrap.Instance.EventMgr.DeliverReceiptFailed -= OnDeliverReceiptFailed;
    }

    public void Init()
    {
        Bootstrap.Instance.EventMgr.DeliverReceiptSuccess += OnDeliverReceiptSuccess;
        Bootstrap.Instance.EventMgr.DeliverReceiptFailed += OnDeliverReceiptFailed;
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
