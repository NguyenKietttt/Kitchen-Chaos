using UnityEngine;

namespace KitchenChaos
{
    public sealed class LookAtCamera : MonoBehaviour
    {
        private enum Mode { LookAt, LookAtInverted, CameraForward, CameraForwardInverted }

        [Header("Property")]
        [SerializeField] private Mode _mode;

        private void LateUpdate()
        {
            LookAt();
        }

        private void LookAt()
        {
            switch (_mode)
            {
                case Mode.LookAt:
                    transform.LookAt(Camera.main.transform);
                    break;
                case Mode.LookAtInverted:
                    transform.LookAt(transform.position + (transform.position - Camera.main.transform.position));
                    break;
                case Mode.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;
                case Mode.CameraForwardInverted:
                    transform.forward = -Camera.main.transform.forward;
                    break;
            }
        }
    }
}
