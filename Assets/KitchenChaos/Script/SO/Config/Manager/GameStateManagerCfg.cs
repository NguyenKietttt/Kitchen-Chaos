using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_GameStateManager", menuName = "Scriptable Object/Config/Manager/GameStateManager")]
    public sealed class GameStateManagerCfg : ScriptableObject
    {
        public GameObject PlayerPrefab => _playerPrefab;
        public GameObject LevelOnePrefab => _levelOnePrefab;

        public float CountdownTimerMin => _countdownTimerMin;
        public float CountdownTimerMax => _countdownTimerMax;
        public float PlayingTimerMin => _playingTimerMin;
        public float PlayingTimerMax => _playingTimerMax;

        [Header("Asset Ref")]
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _levelOnePrefab;

        [Header("Property")]
        [SerializeField] private float _countdownTimerMin;
        [SerializeField] private float _countdownTimerMax;

        [Space]

        [SerializeField] private float _playingTimerMin;
        [SerializeField] private float _playingTimerMax;
    }
}
