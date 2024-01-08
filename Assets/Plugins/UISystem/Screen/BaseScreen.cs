using UnityEngine;

namespace UISystem
{
    public abstract class BaseScreen : MonoBehaviour
    {
        public ScreenID ID => _id;
        public ScreenType Type => _type;

        [Header("Property")]
        [SerializeField] private ScreenID _id;
        [SerializeField] private ScreenType _type;

        public virtual void OnPush(object[] datas = null) { }

        public virtual void OnFocus() { }

        public virtual void OnFocusLost() { }

        public virtual void OnPop() { }
    }
}
