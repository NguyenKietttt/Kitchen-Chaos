using UnityEngine;

public sealed class Bootstrap : MonoBehaviour
{
   public static Bootstrap Instance { get; private set; }

   public EventManager EventMgr { get; private set; }
   public InputManager InputMgr { get; private set; }

   [Header("Objects")]
   [SerializeField] private GameObject[] _sceneObjects;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      
      InitManagers();
      ActiveSceneObjects();
   }

   private void InitManagers()
   {
      EventMgr = new EventManager();
      InputMgr = new InputManager();
   }

   private void ActiveSceneObjects()
   {
      foreach (GameObject sceneObj in _sceneObjects)
      {
         sceneObj.SetActive(true);
      }
   }
}
