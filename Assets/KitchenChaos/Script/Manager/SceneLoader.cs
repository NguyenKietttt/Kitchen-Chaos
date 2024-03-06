using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KitchenChaos
{
    public sealed class SceneLoader : MonoBehaviour
    {
        private readonly WaitForSeconds _waitForHalfSecond = new(0.5f);
        private readonly WaitForEndOfFrame _waitForEndFrame = new();

        public void LoadAsync(SceneState scene, Action? onLoaded = null)
        {
            StartCoroutine(LoadSceneCoroutine(scene, onLoaded));
        }

        private IEnumerator LoadSceneCoroutine(SceneState scene, Action? onLoaded = null)
        {
            yield return _waitForHalfSecond;

            AsyncOperation loadOp = SceneManager.LoadSceneAsync((int)scene);
            loadOp.allowSceneActivation = false;

            yield return _waitForHalfSecond;
            loadOp.allowSceneActivation = true;

            yield return _waitForEndFrame;
            yield return _waitForEndFrame;

            onLoaded?.Invoke();
        }
    }
}
