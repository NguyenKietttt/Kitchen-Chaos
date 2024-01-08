using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public sealed class UIManager : MonoBehaviour
    {
        private const string CANVAS_TAG = "Canvas";

        [Header("Asset Ref")]
        [SerializeField] private ScreenPrefabSO _screenPrefabSO;

        private readonly Stack<BaseScreen> _screenStack = new();

        private Transform _canvas;

        public void Init()
        {
            _screenPrefabSO.Init();
        }

        public void Push(ScreenID id, object[] datas = null)
        {
            if (_canvas == null)
            {
                _canvas = GameObject.FindGameObjectWithTag(CANVAS_TAG).transform;
            }

            BaseScreen nextScreenPrefab = _screenPrefabSO.GetScreenPrefab(id);
            BaseScreen nextScreen = Instantiate(nextScreenPrefab, _canvas);

            if (_screenStack.Count > 0)
            {
                if (nextScreen.Type is ScreenType.FullScreen)
                {
                    PopAll();
                }
                else
                {
                    BaseScreen curScreen = _screenStack.Peek();
                    curScreen.OnFocusLost();
                }
            }

            nextScreen.OnPush(datas);

            _screenStack.Push(nextScreen);
        }

        public void Pop()
        {
            if (_screenStack.Count > 1)
            {
                BaseScreen curScreen = _screenStack.Pop();
                curScreen.OnPop();

                BaseScreen preScreen = _screenStack.Peek();
                preScreen.OnFocus();
            }
            else
            {
                Debug.Log("<color=yellow>UI System: </color>: Only one screen remain, cannot pop!!!");
            }
        }

        private void PopAll()
        {
            while (_screenStack.Count > 0)
            {
                BaseScreen curScreen = _screenStack.Pop();
                curScreen.OnPop();
            }
        }
    }
}
