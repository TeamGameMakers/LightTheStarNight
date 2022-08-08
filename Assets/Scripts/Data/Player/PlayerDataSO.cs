using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public class PlayerDataSO : ScriptableObject
    {
        public float speed;
        
        [Header("Flash Light")]
        public bool hasFlashLight;
        public float lightRadius;
        public float lightAngle;
        public float lightDamage;
        public float transformTime;
        public LayerMask monsterLayer;
        public LayerMask starLightLayer;
        
        [Header("Battery Light")]
        public float maxPower;
        public float powerUsingSpeed;
        public float powerRemaining;

        [Header("Sound")] 
        public AudioClip flashLightOn;
        public AudioClip flashLightOff;
        public AudioClip absorbPower;

        [Header("Die")] 
        public Texture fall;
        public Texture explode;

    }
}