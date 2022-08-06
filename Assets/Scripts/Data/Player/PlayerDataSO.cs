using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerDataSO : ScriptableObject
    {
        public float speed;
        [Space]
        public bool hasFlashLight;
        public float lightRadius;
        public float lightAngle;
        public float lightDamage;
        public LayerMask layer;
        [Space]
        public float maxPower;
        public float powerUsingSpeed;
        public float powerRemaining;
    }
}