using System;
using UnityEngine;

namespace Core
{
    public class GameCore : MonoBehaviour
    {
        public Movement Movement { get; private set; }
        public AIMovement AIMovement { get; private set; }
        public Detection Detection { get; private set; }

        private void Awake()
        {
            Movement = GetComponentInChildren<Movement>();
            AIMovement = GetComponentInChildren<AIMovement>();
            Detection = GetComponentInChildren<Detection>();
            
            if (!Movement && !AIMovement) Debug.LogError("Missing Movement Component In Children");
            if (!Detection) Debug.LogError("Missing Detection Component In Children");
        }

        private void Start()
        {
            if (Movement) Movement.core = this;
            if (AIMovement) AIMovement.core = this;
        }

        public void LogicUpdate()
        {
            // Detection.LogicUpdate();
            if (Movement) Movement.LogicUpdate();
            if (AIMovement) AIMovement.LogicUpdate();
        }
    }
}
