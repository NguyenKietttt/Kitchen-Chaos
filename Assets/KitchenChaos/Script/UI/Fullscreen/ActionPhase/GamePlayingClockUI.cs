using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos
{
    public sealed class GamePlayingClockUI : MonoBehaviour
    {
        [Header("Internal Ref")]
        [SerializeField] private Image _timerImg;

        private void Update()
        {
            _timerImg.fillAmount = Bootstrap.Instance.GameStateMgr.PlayingTimerNormalized;
        }
    }
}
