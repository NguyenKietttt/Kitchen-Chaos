using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_Bootstrap", menuName = "Scriptable Object/Config/Manager/Bootstrap")]
    public sealed class BootstrapCfg : ScriptableObject
    {
        public GameObject PlayerPrefab => _playerPrefab;
        public GameObject LevelOnePrefab => _levelOnePrefab;

        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _levelOnePrefab;
    }
}
