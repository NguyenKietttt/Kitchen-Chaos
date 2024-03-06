using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(menuName = "Scriptable Object/Kitchen Object")]
    public sealed class KitchenObjectSO : ScriptableObject
    {
        public GameObject Prefab => _prefab!;
        public Sprite Sprite => _sprite!;
        public string Name => _name;

        [Header("Asset Ref")]
        [SerializeField] private GameObject? _prefab;
        [SerializeField] private Sprite? _sprite;

        [Header("Property")]
        [SerializeField] private string _name = string.Empty;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void CheckNullEditorReferences()
        {
            if (_prefab == null || _sprite == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }
    }
}
