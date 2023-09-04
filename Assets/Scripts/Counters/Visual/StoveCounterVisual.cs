using UnityEngine;

public sealed class StoveCounterVisual : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private GameObject _stoveOnObj;
    [SerializeField] private GameObject _particlesObj;

    private void Start()
    {
        Bootstrap.Instance.EventMgr.ChangeStoveCounterState += OnStoveCounterStateChanged;
    }

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.ChangeStoveCounterState -= OnStoveCounterStateChanged;
    }

    private void OnStoveCounterStateChanged(StoveCounter.State state)
    {
        bool isActiveVisual = state is StoveCounter.State.Frying or StoveCounter.State.Fried;

        _stoveOnObj.SetActive(isActiveVisual);
        _particlesObj.SetActive(isActiveVisual);
    }
}
