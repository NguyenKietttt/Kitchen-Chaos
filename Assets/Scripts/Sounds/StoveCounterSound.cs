using UnityEngine;

public sealed class StoveCounterSound : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private AudioSource _audioSrc;

    private void Start()
    {
        Bootstrap.Instance.EventMgr.ChangeStoveCounterState += OnStoveCounterState;
    }

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.ChangeStoveCounterState -= OnStoveCounterState;
    }

    private void OnStoveCounterState(StoveCounter.State state)
    {
        bool isPlayedSound = state is StoveCounter.State.Frying or StoveCounter.State.Fried;

        if (isPlayedSound)
        {
            _audioSrc.Play();
        }
        else
        {
            _audioSrc.Stop();
        }
    }
}
