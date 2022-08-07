using System.Collections.Generic;
using Base;
using Core;
using Data;
using UnityEngine;
using Utilities;

namespace Characters
{
    public class Monster: Entity
    {
        [SerializeField] private MonsterDataSO _data;
        private Animator _anim;
        private Collider2D _coll;
        
        public static readonly Dictionary<int, Monster> Monsters;
        
        internal MonsterStateMachine StateMachine { get; private set; }
        internal GameCore Core { get; private set; }
        internal MonsterDataSO Data => _data;
        internal Collider2D target;
        internal Transform SpawnTransform { get; private set; }

        #region States
        
        public MonsterIdleState IdleState { get; private set; }
        public MonsterReturnState ReturnState { get; private set; }
        public MonsterChaseState ChaseState { get; private set; }
        public MonsterPatrolState PatrolState { get; private set; }
        public MonsterDieState DieState { get; private set; }
        
        #endregion
        
        public List<Transform> PatrolPoints { get; private set; }
        public bool Patrol { get; private set; }
        public bool Hit { get; private set; }
        public bool HitByPlayer { get; private set; }

        static Monster()
        {
            Monsters = new Dictionary<int, Monster>();
        }
        
        private void Awake()
        {
            Core = GetComponentInChildren<GameCore>();
            _anim = GetComponent<Animator>();
            _coll = GetComponent<Collider2D>();
            Monsters.Add(_coll.GetInstanceID(), this);
            
            StateMachine = new MonsterStateMachine();
            IdleState = new MonsterIdleState(this, "idle");
            ReturnState = new MonsterReturnState(this, "return");
            ChaseState = new MonsterChaseState(this, "chase");
            PatrolState = new MonsterPatrolState(this, "patrol");
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
            MonsterExitFlashLight();
        }

        private void OnDestroy()
        {
            Monsters.Remove(_coll.GetInstanceID());
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
        public void MonsterEnterLight(float damage, bool hitByPlayer = false)
        {
            Hit = true;
            _data.healthPoint -= damage * Time.deltaTime;
            HitByPlayer = hitByPlayer;
        }

        /// <summary>
        /// 怪离开光
        /// </summary>
        private void MonsterExitFlashLight()
        {
            if (EventCenter.Instance.FuncTrigger<Collider2D, bool>("LightOnMonster", _coll)) return;
            Hit = false;
            HitByPlayer = false;
        }
        
        /// <summary>
        /// 怪离开路灯
        /// </summary>
        private void MonsterExitRoadLight()
        {
            Hit = false;
            HitByPlayer = false;
        }

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