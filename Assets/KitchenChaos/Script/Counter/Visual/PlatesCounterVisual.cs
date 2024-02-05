using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    public sealed class PlatesCounterVisual : MonoBehaviour
    {
        private const float PLATE_OFFSET_Y = 0.1f;
        private const int PLATE_VISUAL_COUNT_MIN = 0;

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
            plateVisual.transform.localPosition = new Vector3(0.0f, PLATE_OFFSET_Y * _plateVisuals.Count, 0.0f);

            _plateVisuals.Push(plateVisual);
        }

        private void OnPlateRemoved()
        {
            if (_plateVisuals.Count > PLATE_VISUAL_COUNT_MIN)
            {
                Destroy(_plateVisuals.Pop());
            }
        }
    }
}
