using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlatesCounterVisual : MonoBehaviour
    {
        private const float PLATE_OFFSET_Y = 0.1f;

        [Header("Asset Ref")]
        [SerializeField] private GameObject _plateVisualPrefab;

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
            Bootstrap.Instance.EventMgr.RemovePlate += OnPlateRemoved;
        }

        private void OnPlateSpawned()
        {
            GameObject plateVisual = Instantiate(_plateVisualPrefab, _spawnPoint);
            plateVisual.transform.localPosition = new Vector3(0, PLATE_OFFSET_Y * _plateVisuals.Count, 0);

            _plateVisuals.Push(plateVisual);
        }

        private void OnPlateRemoved()
        {
            if (_plateVisuals.Count > 0)
            {
                GameObject plateVisual = _plateVisuals.Pop();
                Destroy(plateVisual);
            }
        }
    }
}
