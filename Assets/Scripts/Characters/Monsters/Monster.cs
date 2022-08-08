using System.Collections.Generic;
using Base;
using Base.Resource;
using Core;
using Data;
using Interact;
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
        public MonsterExplosionState ExplosionState { get; private set; }
        public MonsterDieState DieState { get; private set; }
        
        #endregion
        
        // 巡逻
        public List<Transform> PatrolPoints { get; private set; }
        public bool Patrol { get; private set; }
        
        // 受伤判定
        public bool Hit { get; private set; }
        public bool HitByPlayer { get; private set; }
        
        // 爆炸
        public Collider2D explosionCollider;

        static Monster()
        {
            Monsters = new Dictionary<int, Monster>();
        }
        
        private void Awake()
        {
            Core = GetComponentInChildren<GameCore>();
            _anim = GetComponent<Animator>();
            _coll = GetComponent<Collider2D>();
            explosionCollider = GetComponentInChildren<CircleCollider2D>();
            Monsters.Add(_coll.GetInstanceID(), this);
            _data = Instantiate(ResourceLoader.
                Load<MonsterDataSO>(_data.isGood ? "Data/GoodMonsterData" : "Data/BadMonsterData"));
            
            StateMachine = new MonsterStateMachine();
            IdleState = new MonsterIdleState(this, "idle");
            ReturnState = new MonsterReturnState(this, "walk");
            ChaseState = new MonsterChaseState(this, "chase");
            ExplosionState = new MonsterExplosionState(this, "explode");
            PatrolState = new MonsterPatrolState(this, "walk");
            DieState = new MonsterDieState(this);
        }

        private void Start()
        {
            Patrol = CheckPatrol();

            if (!Patrol)
            {
                var parent = transform;
                var go = new GameObject("Spawn Point");
                
                SpawnTransform = go.transform;
                SpawnTransform.position = parent.position;
                SpawnTransform.parent = parent.parent;
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
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();
            
            if (Hit)
                MonsterExitFlashLight();
        }

        private void OnDestroy()
        {
            Monsters.Remove(_coll.GetInstanceID());
        }

        internal void SetAnimBool(int hash, bool value) => _anim.SetBool(hash, value);

        internal void SetAnimFloat(int hash, float value) => _anim.SetFloat(hash, value);

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
            if (EventCenter.Instance.EventTrigger<Collider2D, bool>("LightOnMonster", _coll)) return;
            Hit = false;
            HitByPlayer = false;
        }
        
        // Animation Event
        public void Explode() => ExplosionState.Explode();
        

        public void MonsterDie()
        {
            var curTransform = transform;
            
            _data.isDead = true;
            
            // 生成星光
            if (StateMachine.CurrentState == DieState)
            {
                var go = ResourceLoader.Load<GameObject>("Prefabs/Interactable/StarLight_T");
                go.transform.position = transform.position;
                go.GetComponent<RambleObject>().UpdatePosition();
                // Vector3 pos = transform.position;
                // ResourceLoader.LoadAsync<GameObject>("Prefabs/Interactable/StarLight_T", o => {
                //     o.transform.position = pos;
                //     o.transform.GetComponent<RambleObject>().UpdatePosition();
                // });
            }
            
            var parent = curTransform.parent;
            curTransform.parent = null;
            
            Destroy(parent.gameObject);
            Destroy(SpawnTransform.gameObject);
            Destroy(curTransform.gameObject);
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