using UnityEngine;

public sealed class Bootstrap : MonoBehaviour
{
    public static Bootstrap Instance { get; private set; }

    public EventManager EventMgr { get; private set; }
    public GameStateManager GameStateMgr { get; private set; }
    public InputManager InputMgr { get; private set; }
    public DeliveryManager DeliveryMgr { get; private set; }
    public SceneLoader SceneLoader => _sceneLoader;
    public SFXManager SFXMgr => _sfxMgr;
    public MusicManager MusicMgr => _musicMgr;

    [Header("Asset Ref")]
    [SerializeField] private ListReceiptSO _receiptSOList;

    [Header("Internal Ref")]
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private SFXManager _sfxMgr;
    [SerializeField] private MusicManager _musicMgr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        InitManagers();
        _sceneLoader.LoadAsync(SceneLoader.Scene.MainMenu);
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
        GameStateMgr.OnDestroy();
    }

    private void InitManagers()
    {
        EventMgr = new EventManager();
        GameStateMgr = new GameStateManager();
        InputMgr = new InputManager();

        DeliveryMgr = new DeliveryManager(_receiptSOList);
    }
}
