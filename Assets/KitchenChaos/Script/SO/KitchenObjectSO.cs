using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(menuName = "ScriptableObject/KitchenObject")]
    public sealed class KitchenObjectSO : ScriptableObject
    {
        public GameObject Prefab => _prefab;
        public Sprite Sprite => _sprite;
        public string Name => _name;

        [SerializeField] private GameObject _prefab;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;
    }
}
