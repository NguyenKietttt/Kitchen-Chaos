using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(fileName = "SO_ScreenPrefab", menuName = "Scriptable Object/UI System/Screen Prefab Ref")]
    public sealed class ScreenPrefabSO : ScriptableObject
    {
        [SerializeField] private BaseScreen[] _screenPrefabs;

        private Dictionary<ScreenID, BaseScreen> _screenPrefabDic;

        public void Init()
        {
            if (_screenPrefabDic?.Count > 0)
            {
                return;
            }

            _screenPrefabDic = new Dictionary<ScreenID, BaseScreen>(_screenPrefabs.Length);
            for (int i = 0; i < _screenPrefabs.Length; i++)
            {
                BaseScreen screen = _screenPrefabs[i];
                if (!_screenPrefabDic.ContainsKey(screen.ID))
                {
                    _screenPrefabDic.Add(screen.ID, screen);
                }
            }
        }

        public BaseScreen GetScreenPrefab(ScreenID id)
        {
            if (_screenPrefabDic.TryGetValue(id, out BaseScreen screen))
            {
                return screen;
            }

            return null;
        }
    }
}
