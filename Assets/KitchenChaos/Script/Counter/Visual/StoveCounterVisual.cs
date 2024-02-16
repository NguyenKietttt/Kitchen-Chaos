using UnityEngine;

namespace KitchenChaos
{
    public sealed class StoveCounterVisual : MonoBehaviour
    {
        [Header("External Ref")]
        [SerializeField] private GameObject _stoveCounterObj;

        [Header("Internal Ref")]
        [SerializeField] private GameObject _stoveOnObj;
        [SerializeField] private GameObject _particlesObj;

        private void Start()
        {
            Bootstrap.Instance.EventMgr.ChangeStoveCounterState += OnStoveCounterStateChanged;
        }

        private void OnDestroy()
        {
            Bootstrap.Instance.EventMgr.ChangeStoveCounterState -= OnStoveCounterStateChanged;
        }

        private void OnStoveCounterStateChanged(int senderID, StoveCounterState state)
        {
            if (_stoveCounterObj.GetInstanceID() != senderID)
            {
                return;
            }

            bool isActiveVisual = state is StoveCounterState.Frying or StoveCounterState.Fried;
            _stoveOnObj.SetActive(isActiveVisual);
            _particlesObj.SetActive(isActiveVisual);
        }
    }
}
