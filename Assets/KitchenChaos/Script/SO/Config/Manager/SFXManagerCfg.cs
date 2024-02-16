using System;
using System.Collections.Generic;
using UnityEngine;
using Victor.Tools;

namespace KitchenChaos
{
    [CreateAssetMenu(fileName = "Cfg_SFXManager", menuName = "Scriptable Object/Config/Manager/SFXManager")]
    public sealed class SFXManagerCfg : ScriptableObject
    {
        public IReadOnlyList<AudioClip> ChopClips => Array.AsReadOnly(_chopClips);
        public IReadOnlyList<AudioClip> DeliveryFailClips => Array.AsReadOnly(_deliveryFailClips);
        public IReadOnlyList<AudioClip> DeliverySuccessClips => Array.AsReadOnly(_deliverySuccessClips);
        public IReadOnlyList<AudioClip> FootstepClips => Array.AsReadOnly(_footstepClips);
        public IReadOnlyList<AudioClip> ObjectDropClips => Array.AsReadOnly(_objectDropClips);
        public IReadOnlyList<AudioClip> ObjectPickupClips => Array.AsReadOnly(_objectPickupClips);
        public IReadOnlyList<AudioClip> TrashClips => Array.AsReadOnly(_trashClips);
        public IReadOnlyList<AudioClip> WarningClips => Array.AsReadOnly(_warningClips);

        public string PlayerPrefsVolumnKey => _playerPrefsVolumnKey;

        public float VolumnMin => _volumnMin;
        public float VolumnMax => _volumnMax;
        public float DefaultVolumn => _defaultVolumn;
        public float VolumnStep => _volumnStep;

        public float ChopVolume => _chopVolume;
        public float PickupVolume => _pickupVolume;
        public float DropVolume => _dropVolume;
        public float TrashVolume => _trashVolume;
        public float DeliverySuccessVolume => _deliverySuccessVolume;
        public float DeliveryFailedVolume => _deliveryFailedVolume;
        public float CountdownPopupVolume => _countdownPopupVolume;

        [Header("Asset Ref")]
        [SerializeField] private AudioClip[] _chopClips;
        [SerializeField] private AudioClip[] _deliveryFailClips;
        [SerializeField] private AudioClip[] _deliverySuccessClips;
        [SerializeField] private AudioClip[] _footstepClips;
        [SerializeField] private AudioClip[] _objectDropClips;
        [SerializeField] private AudioClip[] _objectPickupClips;
        [SerializeField] private AudioClip[] _trashClips;
        [SerializeField] private AudioClip[] _warningClips;

        [Header("Property")]
        [SerializeField] private string _playerPrefsVolumnKey;

        [Space]

        [SerializeField] private float _volumnMin;
        [SerializeField] private float _volumnMax;
        [SerializeField] private float _defaultVolumn;
        [SerializeField] private float _volumnStep;

        [Space]

        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _chopVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _pickupVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _dropVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _trashVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _deliverySuccessVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _deliveryFailedVolume;
        [SerializeField][VTRangeStep(0.0f, 1.0f, 0.1f)] private float _countdownPopupVolume;
    }
}
