using UnityEngine;

namespace KitchenChaos
{
    public sealed class LookAtCamera : MonoBehaviour
    {
        [Header("Property")]
        [SerializeField] private LookAtCameraMode _mode;

        private void LateUpdate()
        {
            LookAt();
        }

        private void LookAt()
        {
            switch (_mode)
            {
                case LookAtCameraMode.LookAt:
                    transform.LookAt(Camera.main.transform);
                    break;
                case LookAtCameraMode.LookAtInverted:
                    transform.LookAt(transform.position + (transform.position - Camera.main.transform.position));
                    break;
                case LookAtCameraMode.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;
                case LookAtCameraMode.CameraForwardInverted:
                    transform.forward = -Camera.main.transform.forward;
                    break;
            }
        }
    }
}
