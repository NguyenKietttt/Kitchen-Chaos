using UnityEngine;

public sealed class ClearCounter : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    [Header("References")]
    [SerializeField] private GameObject _selectedVisual;
    [SerializeField] private Transform _spawnPoint;

    private void OnEnable()
    {
        Bootstrap.Instance.EventMgr.OnSelectCounter += OnSelected;
    }

    private void OnDisable()
    {
        Bootstrap.Instance.EventMgr.OnSelectCounter -= OnSelected;
    }

    public void OnInteract()
    {
        KitchenObject kitchenObj = Instantiate(_kitchenObjectSO.Prefab, parent: _spawnPoint).GetComponent<KitchenObject>();
        Debug.Log(kitchenObj.GetKitchenObjectSO().Name);
    }

    private void OnSelected(ClearCounter selectedCounter)
    {
        _selectedVisual.SetActive(selectedCounter == this);
    }
}