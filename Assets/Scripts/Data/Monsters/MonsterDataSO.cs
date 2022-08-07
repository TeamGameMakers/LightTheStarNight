using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Data/Monster")]
    public class MonsterDataSO: ScriptableObject
    {
        public bool isGood;
        public bool isDead;
        public float fadeSpeed;

        [Header("Attribute")]
        public float walkSpeed;
        public float chaseSpeed;
        public float hitSpeed;
        public float healthPoint;

        [Header("Detection")] 
        public float checkRadius;
        public float checkAngle;
        public LayerMask checkLayer;
        
        [Header("Patrol")]
        public float _patrolStopTime;

        [Header("Explosion")] 
        public float explosionDistance;
        public ContactFilter2D filter;
    }
}