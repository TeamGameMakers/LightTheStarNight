using System;
using UnityEngine;
using Pathfinding;

namespace Core
{
    public class AIMovement: CoreComponent
    {
        private IAstarAI _ai;

        private Vector2 _currentVelocity;
        private Transform _currentDestination;
        
        public Vector2 CurrentVelocity => _currentVelocity;
        public Vector2 CurrentVelocityNorm => _currentVelocity.normalized;
        public float CurrentSpeed => _currentVelocity.magnitude;
        public Transform CurrentDestination { get => _currentDestination; set => _currentDestination = value; }

        private void Awake()
        {
            _ai = GetComponentInParent<IAstarAI>();
        }

        private void OnEnable()
        {
            _ai.onSearchPath += Update;
        }

        private void Start()
        {
            _currentDestination = transform;
        }

        internal void LogicUpdate()
        {
            _currentVelocity = _ai.desiredVelocity;
        }
        
        private void OnDisable()
        {
            _ai.onSearchPath -= Update;
        }
        
        // 不知道为啥，反正 onSearchPath必须接收 Update
        private void Update()
        {
            if (_currentDestination) 
                _ai.destination = _currentDestination.position;
        }

        public void SetSpeed(float speed) => _ai.maxSpeed = speed;
        public void StopMoving() => _ai.isStopped = true;
    }
}