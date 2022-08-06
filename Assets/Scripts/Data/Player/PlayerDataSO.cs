using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerDataSO : ScriptableObject
    {
        public float walkVelocity;
        public float runVelocity;
        public int healthPoint;

        public bool hasFlashLight;
        public int batteryNum;
    }
}
