using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LoadingUI : MonoBehaviour
{
    private const int GAMEPLAY_SCENE_INDEX = 3;

    private void Start()
    {
        StartCoroutine(LoadGameSceneAsync());
    }

    private IEnumerator LoadGameSceneAsync()
    {
        yield return new WaitForSeconds(0.5f);

        AsyncOperation loadOp = SceneManager.LoadSceneAsync(GAMEPLAY_SCENE_INDEX);
        loadOp.allowSceneActivation = false;

        Debug.Log("Loading completed! Transitioning");

        yield return new WaitForSeconds(0.5f);
        loadOp.allowSceneActivation = true;

        yield return new WaitForEndOfFrame();
        Bootstrap.Instance.GameStateMgr.Init();
    }
}
