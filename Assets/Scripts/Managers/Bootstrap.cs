using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Bootstrap : MonoBehaviour
{
    public static Bootstrap Instance { get; private set; }

    public EventManager EventMgr { get; private set; }
    public InputManager InputMgr { get; private set; }
    public DeliveryManager DeliveryMgr { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        InitManagers();
        StartCoroutine(LoadGameSceneAsync());
    }

    private void Update()
    {
        DeliveryMgr.OnUpdate();
    }

    private void OnDestroy()
    {
        InputMgr.OnDestroy();
    }

    private void InitManagers()
    {
        EventMgr = new EventManager();
        InputMgr = new InputManager();
        DeliveryMgr = new DeliveryManager();
    }

    private IEnumerator LoadGameSceneAsync()
    {
        yield return new WaitForSeconds(0.5f);

        AsyncOperation loadOp = SceneManager.LoadSceneAsync(1);
        loadOp.allowSceneActivation = false;

        Debug.Log("Loading completed! Transitioning");

        yield return new WaitForSeconds(0.5f);

        loadOp.allowSceneActivation = true;
    }
}
