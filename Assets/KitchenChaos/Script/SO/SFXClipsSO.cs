using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "SO_SFXClips", menuName = "Scriptable Object/SFX Clips")]
    public sealed class SFXClipsSO : ScriptableObject
    {
        public IReadOnlyList<AudioClip> Chop => Array.AsReadOnly(_chop);
        public IReadOnlyList<AudioClip> DeliveryFail => Array.AsReadOnly(_deliveryFail);
        public IReadOnlyList<AudioClip> DeliverySuccess => Array.AsReadOnly(_deliverySuccess);
        public IReadOnlyList<AudioClip> Footstep => Array.AsReadOnly(_footstep);
        public IReadOnlyList<AudioClip> ObjectDrop => Array.AsReadOnly(_objectDrop);
        public IReadOnlyList<AudioClip> ObjectPickup => Array.AsReadOnly(_objectPickup);
        public IReadOnlyList<AudioClip> Trash => Array.AsReadOnly(_trash);
        public IReadOnlyList<AudioClip> Warning => Array.AsReadOnly(_warning);

        [Header("Asset Ref")]
        [SerializeField] private AudioClip[] _chop;
        [SerializeField] private AudioClip[] _deliveryFail;
        [SerializeField] private AudioClip[] _deliverySuccess;
        [SerializeField] private AudioClip[] _footstep;
        [SerializeField] private AudioClip[] _objectDrop;
        [SerializeField] private AudioClip[] _objectPickup;
        [SerializeField] private AudioClip[] _trash;
        [SerializeField] private AudioClip[] _warning;
    }
}
