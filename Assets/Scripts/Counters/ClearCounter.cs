using System;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class ClearCounter : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private KitchenObjectSO _kitchenObjSO;

    [Header("References")]
    [SerializeField] private GameObject _selectedVisual;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ClearCounter _secondClearCounter;
    [SerializeField] private bool _isTesting;

    private KitchenObject _kitchenObj;

    private void OnEnable()
    {
        Bootstrap.Instance.EventMgr.OnSelectCounter += OnSelected;
    }

    private void Update()
    {
        if (_isTesting && Input.GetKeyDown(KeyCode.T))
        {
            if (_kitchenObj != null)
            {
                _kitchenObj.SetCurClearCounter(_secondClearCounter);
            }
        }
    }

    private void OnDisable()
    {
        Bootstrap.Instance.EventMgr.OnSelectCounter -= OnSelected;
    }

    public void OnInteract()
    {
        if (_kitchenObj == null)
        {
            _kitchenObj = Instantiate(_kitchenObjSO.Prefab, parent: _spawnPoint).GetComponent<KitchenObject>();
            _kitchenObj.SetCurClearCounter(this);
        }
    }

    public Transform GetCounterSpawnPoint()
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