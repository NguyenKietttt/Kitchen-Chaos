using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjParent
{
    [Header("SO")]
    [SerializeField] protected KitchenObjectSO _kitchenObjSO;

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

    public virtual void OnInteract(PlayerController playerController) { }

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

    private void OnSelected(BaseCounter selectedCounter)
    {
        _selectedVisual.SetActive(selectedCounter == this);
    }
}
