using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_GameStateManager", menuName = "Scriptable Object/Config/Manager/GameStateManager")]
    public sealed class GameStateManagerCfg : ScriptableObject
    {
        public GameObject PlayerPrefab => _playerPrefab;
        public GameObject LevelOnePrefab => _levelOnePrefab;

        public float CoundownTimerMin => _coundownTimerMin;
        public float CoundownTimerMax => _coundownTimerMax;
        public float PlayingTimerMin => _playingTimerMin;
        public float PlayingTimerMax => _playingTimerMax;

        [Header("Asset Ref")]
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _levelOnePrefab;

        [Header("Property")]
        [SerializeField] private float _coundownTimerMin;
        [SerializeField] private float _coundownTimerMax;
        [SerializeField] private float _playingTimerMin;
        [SerializeField] private float _playingTimerMax;
    }
}
