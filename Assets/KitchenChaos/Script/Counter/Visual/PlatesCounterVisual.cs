using System.Collections.Generic;
using UnityEngine;
using UnityServiceLocator;

namespace KitchenChaos
{
    public sealed class PlatesCounterVisual : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlateCounterVisualCfg _config;

        [Header("External Ref")]
        [SerializeField] private Transform _spawnPoint;

        private readonly Stack<GameObject> _plateVisuals = new();

        private EventManager _eventMgr;

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DeregisterServices();
        }

        private void OnPlateSpawned()
        {
            GameObject plateVisual = Instantiate(_config.PlateVisualPrefab, _spawnPoint);
            plateVisual.transform.localPosition = new Vector3(0.0f, _config.PlateOffsetY * _plateVisuals.Count, 0.0f);

            _plateVisuals.Push(plateVisual);
        }

        private void OnPlateRemoved()
        {
            if (_plateVisuals.Count > _config.PlateAmountMin)
            {
                Destroy(_plateVisuals.Pop());
            }
        }

        private void RegisterServices()
        {
            _eventMgr = ServiceLocator.Instance.Get<EventManager>();
        }

        private void DeregisterServices()
        {
            _eventMgr = null;
        }

        private void SubscribeEvents()
        {
            _eventMgr.SpawnPlate += OnPlateSpawned;
            _eventMgr.RemovePlate += OnPlateRemoved;
        }

        private void UnsubscribeEvents()
        {
            _eventMgr.SpawnPlate -= OnPlateSpawned;
            _eventMgr.RemovePlate -= OnPlateRemoved;
        }
    }
}
