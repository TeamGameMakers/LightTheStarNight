using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Security Camera Data", menuName = "Data/Security Camera")]
    public class SecurityCameraDataSO: InteractableDataSO
    {
        [Header("Security Camera Settings")]
        public float rotationSpeed;
        public float maxAngle;
    }
}