using KitchenChaos.Utils;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_MainMenuUI", menuName = "Scriptable Object/Config/UI/Main Menu UI")]
    public sealed class MainMenuUICfg : ScriptableObject
    {
        public GameObject DecorationPrefab => _decorationPrefab!;

        [Header("Asset Ref")]
        [SerializeField] private GameObject? _decorationPrefab;

        private void OnValidate()
        {
            CheckNullEditorReferences();
        }

        private void CheckNullEditorReferences()
        {
            if (_decorationPrefab == null)
            {
                CustomLog.LogError(this, "missing references in editor!!!");
            }
        }
    }
}
