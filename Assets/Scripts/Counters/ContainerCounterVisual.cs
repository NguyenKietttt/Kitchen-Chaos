using UnityEngine;

public sealed class ContainerCounterVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;

    private readonly int _openCloseKeyHash = Animator.StringToHash("OpenClose");

    private void OnEnable()
    {
        Bootstrap.Instance.EventMgr.OnPlayerGrabObj += OnPlayerGrabObj;
    }

    private void OnDisable()
    {
        Bootstrap.Instance.EventMgr.OnPlayerGrabObj -= OnPlayerGrabObj;
    }

    private void OnPlayerGrabObj()
    {
        _animator.SetTrigger(_openCloseKeyHash);
    }
}
