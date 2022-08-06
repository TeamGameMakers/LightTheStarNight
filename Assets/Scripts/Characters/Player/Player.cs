using System.Collections.Generic;
using Base;
using Core;
using Data;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Characters
{
    public class Player : Entity
    {
        internal PlayerStateMachine StateMachine { get; private set; }
        internal Animator Anim { get; private set; }
        internal GameCore Core { get; private set; }
        
        private Light2D _flashLight;
        private SpriteRenderer _sprite;
        private List<Collider2D> _monstersColl;
        
        public PlayerDataSO data;
        public RuntimeAnimatorController playerWithFlashLight;

        #region States
    
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
    
        #endregion

        private void Awake()
        {
            Anim = GetComponent<Animator>();
            Core = GetComponentInChildren<GameCore>();
            _flashLight = transform.GetChild(2).GetComponent<Light2D>();
            _sprite = GetComponent<SpriteRenderer>();

            StateMachine = new PlayerStateMachine();
            IdleState = new PlayerIdleState(this, "idle");
            MoveState = new PlayerMoveState(this, "move");
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
            GM.GameManager.SetPlayerTransform(transform);
            
            // 手电筒
            _flashLight.pointLightOuterRadius = data.lightRadius;
            _flashLight.pointLightOuterAngle = data.lightAngle;
            _flashLight.pointLightInnerAngle = data.lightAngle - 10;
            _flashLight.enabled = false;
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void Update()
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();
            
            if (data.hasFlashLight) Anim.runtimeAnimatorController = playerWithFlashLight;

            FlashLightControl();
            
            // 手电伤害判定
            if (_flashLight.enabled)
            {
                _monstersColl = Core.Detection.ArcDetectionAll(Core.Detection.transform, 
                    data.lightRadius, data.lightAngle * 0.5f, data.layer);
                
                foreach (var coll in _monstersColl)
                    Monster.Monsters[coll.GetInstanceID()].MonsterEnterLight(data.lightDamage, true);
            }
        }
        
        private void OnDisable()
        {
            EventCenter.Instance.RemoveEventListener<Collider2D, bool>("LightOnMonster", LightOnMonster);
        }
        
        private void FlashLightControl()
        {
            if (InputHandler.LightPressed && data.hasFlashLight)
            {
                InputHandler.UseLightInput();
                
                if (data.powerRemaining > 0 && !_flashLight.enabled)
                    _flashLight.enabled = true;
                else
                    _flashLight.enabled = false;
            }
            else if (_flashLight.enabled && data.powerRemaining <= 0)
                _flashLight.enabled = false;

            if (_flashLight.enabled)
                data.powerRemaining -= data.powerUsingSpeed * Time.deltaTime;

            _sprite.sortingLayerName = Anim.GetFloat(MoveState.animHashFloatY) > 0 ? "PlayerUnLit" : "PlayerLit";
        }
        
        private bool LightOnMonster(Collider2D coll) => _monstersColl.Contains(coll);
    }
}
