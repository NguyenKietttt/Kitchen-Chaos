using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Bootstrap : MonoBehaviour
{
    public static Bootstrap Instance { get; private set; }

    private const int MAIN_MENU_SCENE_INDEX = 1;

    public EventManager EventMgr { get; private set; }
    public GameStateManager GameStateMgr { get; private set; }
    public InputManager InputMgr { get; private set; }
    public DeliveryManager DeliveryMgr { get; private set; }
    public SFXManager SFXMgr => _sfxMgr;

    [Header("Internal Ref")]
    [SerializeField] private SFXManager _sfxMgr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        InitManagers();
        StartCoroutine(LoadMainMenuSceneAsync());
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        GameStateMgr.OnUpdate(deltaTime);
        DeliveryMgr.OnUpdate(deltaTime);
    }

    private void OnDestroy()
    {
        InputMgr.OnDestroy();
    }

    private void InitManagers()
    {
        EventMgr = new EventManager();
        GameStateMgr = new GameStateManager();
        InputMgr = new InputManager();

        _sfxMgr.Init();

        DeliveryMgr = new DeliveryManager();
    }

    private IEnumerator LoadMainMenuSceneAsync()
    {
        yield return new WaitForSeconds(0.5f);

        AsyncOperation loadOp = SceneManager.LoadSceneAsync(MAIN_MENU_SCENE_INDEX);
        loadOp.allowSceneActivation = false;

        Debug.Log("Loading completed! Transitioning");

        yield return new WaitForSeconds(0.5f);
        loadOp.allowSceneActivation = true;

        yield return new WaitForEndOfFrame();
    }
}
