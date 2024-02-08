using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlatesCounterVisual : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private PlateCounterVisualCfg _config;

        [Header("External Ref")]
        [SerializeField] private Transform _spawnPoint;

        private readonly Stack<GameObject> _plateVisuals = new();

        private void Start()
        {
            Bootstrap.Instance.EventMgr.SpawnPlate += OnPlateSpawned;
            Bootstrap.Instance.EventMgr.RemovePlate += OnPlateRemoved;
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.SpawnPlate -= OnPlateSpawned;
            Bootstrap.Instance.EventMgr.RemovePlate -= OnPlateRemoved;
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
    }
}
