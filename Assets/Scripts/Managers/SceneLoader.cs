using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneLoader : MonoBehaviour
{
    public enum Scene
    {
        Intro = 0,
        MainMenu = 1,
        Loading = 2,
        Gameplay = 3
    }

    private readonly WaitForSeconds _waitForHalfSecond = new(0.5f);
    private readonly WaitForEndOfFrame _waitForEndFrame = new();

    public void Load(Scene scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public void LoadAsync(Scene scene, Action onLoaded = null)
    {
        StartCoroutine(LoadSceneCoroutine(scene, onLoaded));
    }

    private IEnumerator LoadSceneCoroutine(Scene scene, Action onLoaded = null)
    {
        yield return _waitForHalfSecond;

        AsyncOperation loadOp = SceneManager.LoadSceneAsync((int)scene);
        loadOp.allowSceneActivation = false;

        Debug.Log($"{scene} is loaded! Transitioning");

        yield return _waitForHalfSecond;
        loadOp.allowSceneActivation = true;

        yield return _waitForEndFrame;

        onLoaded?.Invoke();
    }
}
