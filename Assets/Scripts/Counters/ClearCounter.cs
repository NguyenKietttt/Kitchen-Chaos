using UnityEngine;

public sealed class ClearCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _selectedVisual;

    private void OnEnable()
    {
        Bootstrap.Instance.EventMgr.OnSelectCounter += OnSelected;
    }

    private void OnDisable()
    {
        Bootstrap.Instance.EventMgr.OnSelectCounter -= OnSelected;
    }

    private void OnSelected(ClearCounter selectedCounter)
    {
        _selectedVisual.SetActive(selectedCounter == this);
    }
}