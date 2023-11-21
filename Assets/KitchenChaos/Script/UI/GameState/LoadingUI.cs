using UnityEngine;

public sealed class LoadingUI : MonoBehaviour
{
    private void Start()
    {
        Bootstrap.Instance.SceneLoader.LoadAsync(SceneLoader.Scene.Gameplay, () => Bootstrap.Instance.GameStateMgr.Init());
    }
}
