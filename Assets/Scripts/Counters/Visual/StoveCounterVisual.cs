using UnityEngine;

public sealed class StoveCounterVisual : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private GameObject _stoveOnGameObj;
    [SerializeField] private GameObject _particlesGameObj;

    private void Start()
    {
        Bootstrap.Instance.EventMgr.OnStoveCounterStateChanged += OnStoveCounterStateChanged;
    }

    private void OnDestroy()
    {
        Bootstrap.Instance.EventMgr.OnStoveCounterStateChanged -= OnStoveCounterStateChanged;
    }

    private void OnStoveCounterStateChanged(StoveCounter.State state)
    {
        bool isActiveVisual = state is StoveCounter.State.Frying or StoveCounter.State.Fried;

        _stoveOnGameObj.SetActive(isActiveVisual);
        _particlesGameObj.SetActive(isActiveVisual);
    }
}
