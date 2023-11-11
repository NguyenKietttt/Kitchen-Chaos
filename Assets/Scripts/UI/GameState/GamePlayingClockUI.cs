using UnityEngine;
using UnityEngine.UI;

public sealed class GamePlayingClockUI : MonoBehaviour
{
    [Header("Internal Ref")]
    [SerializeField] private Image _timerImg;

    private void Update()
    {
        _timerImg.fillAmount = Bootstrap.Instance.GameStateMgr.GamePlayingTimerNormalized;
    }
}
