using UnityEngine;

public sealed class ClearCounter : MonoBehaviour, IKitchenObjParent
{
    [Header("Prefabs")]
    [SerializeField] private KitchenObjectSO _kitchenObjSO;

    [Header("References")]
    [SerializeField] private GameObject _selectedVisual;
    [SerializeField] private Transform _spawnPoint;

    private KitchenObject _kitchenObj;

    private void OnEnable()
    {
        Bootstrap.Instance.EventMgr.OnSelectCounter += OnSelected;
    }

    private void OnDisable()
    {
        Bootstrap.Instance.EventMgr.OnSelectCounter -= OnSelected;
    }

    public void OnInteract(PlayerController playerController)
    {
        if (_kitchenObj == null)
        {
            _kitchenObj = Instantiate(_kitchenObjSO.Prefab, parent: _spawnPoint).GetComponent<KitchenObject>();
            _kitchenObj.SetCurKitchenObjParent(this);
        }
        else
        {
            _kitchenObj.SetCurKitchenObjParent(playerController);
        }
    }

    public Transform GetSpawnPoint()
    {
        return _spawnPoint;
    }

    public KitchenObject GetKitchenObj()
    {
        return _kitchenObj;
    }

    public void SetKitchenObj(KitchenObject newKitchenObj)
    {
        _kitchenObj = newKitchenObj;
    }

    public bool HasKitchenObj()
    {
        return _kitchenObj != null;
    }

    private void OnSelected(ClearCounter selectedCounter)
    {
        _selectedVisual.SetActive(selectedCounter == this);
    }
}