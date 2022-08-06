using System.Collections.Generic;
using Base.FSM;
using Core;
using Data;
using Utilities;
using UnityEngine;

namespace Characters.Monsters
{
    public class Monster: Entity
    {
        [SerializeField] private MonsterDataSO _data;
        private Animator _anim;
        
        internal MonsterStateMachine StateMachine { get; private set; }
        internal GameCore Core { get; private set; }
        internal MonsterDataSO Data => _data;
        internal Collider2D target;
        internal Transform SpawnTransform { get; private set; }
        
        public MonsterIdleState IdleState { get; private set; }
        public MonsterChaseState ChaseState { get; private set; }
        public MonsterPatrolState PatrolState { get; private set; }
        public MonsterDieState DieState { get; private set; }
        
        public List<Transform> PatrolPoints { get; private set; }
        public bool Patrol { get; private set; }
        public bool Hit { get; private set; }
        public bool HitByPlayer { get; private set; }
        public int Damage { get; private set; }

        private void Awake()
        {
            Core = GetComponentInChildren<GameCore>();
            _anim = GetComponent<Animator>();
            
            StateMachine = new MonsterStateMachine();
            IdleState = new MonsterIdleState(this);
            ChaseState = new MonsterChaseState(this);
            PatrolState = new MonsterPatrolState(this);
            DieState = new MonsterDieState(this);
        }

        private void Start()
        {
            Patrol = CheckPatrol();

            if (!Patrol)
            {
                SpawnTransform = Instantiate(new GameObject("Spawn Point"), transform.parent).transform;
                SpawnTransform.right = Core.Detection.transform.right;
            }

            if (_data.isDead)
                MonsterDie();
            
            StateMachine.Initialize(IdleState);
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
        }

        internal void SetAnimBool(int hash, bool value)
        {
            _anim.SetBool(hash, value);
        }

        private bool CheckPatrol()
        {
            if (transform.parent.childCount < 2) return false;
            var points = transform.parent.GetChild(1);
            if (points.childCount < 2) return false;
            
            PatrolPoints = new List<Transform>();
            for (int i = 0; i < points.childCount; i++)
                PatrolPoints.Add(points.GetChild(i));
            
            return true;
        }
        
        /// <summary>
        /// 怪进入光
        /// </summary>
        public void MonsterEnterLight(int damage, bool hitByPlayer = false)
        {
            Hit = true;
            Damage = damage;
            HitByPlayer = hitByPlayer;
        }

        /// <summary>
        /// 怪离开光
        /// </summary>
        public void MonsterExitLight() => Hit = false;
        
        public void MonsterDie()
        {
            _data.isDead = true;
            Destroy(Patrol ? transform.parent.gameObject : transform.gameObject);
        }

#if UNITY_EDITOR
        
        private void OnDrawGizmos()
        {
            var origin = Application.isPlaying ? Core.Detection.transform : transform;
            Utils.DrawWireArc2D(origin, _data.checkRadius, _data.checkAngle, Color.green);
        }
        
#endif
    }
}