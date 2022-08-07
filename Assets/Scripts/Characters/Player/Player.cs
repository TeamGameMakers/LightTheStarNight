using System.Collections.Generic;
using Base;
using Base.Resource;
using Core;
using Data;
using Interact;
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
        private AudioSource _audio;
        private List<Collider2D> _monstersColl;
        private List<Collider2D> _starLightsColl;
        
        public PlayerDataSO data;
        public RuntimeAnimatorController playerWithFlashLight;
        public Vector2 environmentVelocity = Vector2.zero;

        #region States
    
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }

        #endregion

        private void Awake()
        {
            Anim = GetComponent<Animator>();
            Core = GetComponentInChildren<GameCore>();
            _audio = GetComponent<AudioSource>();
            _flashLight = transform.GetChild(2).GetComponent<Light2D>();
            _sprite = GetComponent<SpriteRenderer>();

            data = Instantiate(ResourceLoader.Load<PlayerDataSO>("Data/PlayerData"));

            StateMachine = new PlayerStateMachine();
            IdleState = new PlayerIdleState(this, "idle");
            MoveState = new PlayerMoveState(this, "move");
        }

        private void OnEnable()
        {
            EventCenter.Instance.AddEventListener<Collider2D, bool>("LightOnMonster", LightOnMonster);
            EventCenter.Instance.AddEventListener<Collider2D, bool>("LightOnStarLight", LightOnStarLight);
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
            
            // 手电判定
            FlashLightDetect();
        }

        private void Update()
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();
            
            if (data.hasFlashLight) Anim.runtimeAnimatorController = playerWithFlashLight;

            FlashLightControl();
        }
        
        private void OnDisable()
        {
            EventCenter.Instance.RemoveEventListener<Collider2D, bool>("LightOnMonster", LightOnMonster);
            EventCenter.Instance.RemoveEventListener<Collider2D, bool>("LightOnStarLight", LightOnStarLight);
        }
        
        private void FlashLightControl()
        {
            if (InputHandler.LightPressed && data.hasFlashLight)
            {
                InputHandler.UseLightInput();

                if (data.powerRemaining > 0 && !_flashLight.enabled)
                {
                    _flashLight.enabled = true;
                    PlayerAudioClip(data.flashLightOn);
                }
                else
                {
                    _flashLight.enabled = false;
                    _starLightsColl = _monstersColl = new List<Collider2D>();
                    PlayerAudioClip(data.flashLightOff);
                }
            }
            else if (_flashLight.enabled && data.powerRemaining <= 0)
                _flashLight.enabled = false;

            if (_flashLight.enabled)
            {
                data.powerRemaining -= data.powerUsingSpeed * Time.deltaTime;
                data.powerRemaining = Mathf.Clamp(data.powerRemaining, 0, data.maxPower);
            }

            _sprite.sortingLayerName = Anim.GetFloat(MoveState.animHashFloatY) > 0 ? "PlayerUnLit" : "PlayerLit";
            
            // UI更新
            var powerUsed = Mathf.FloorToInt((data.maxPower - data.powerRemaining) / data.maxPower * 4);
            if (powerUsed >= 0) EventCenter.Instance.EventTrigger("UpdateBatteryUI", powerUsed);
        }

        private void FlashLightDetect()
        {
            if (!_flashLight.enabled) return;
            
            _monstersColl = Core.Detection.ArcDetectionAll(Core.Detection.transform, 
                data.lightRadius, data.lightAngle * 0.5f, data.monsterLayer);
                
            _starLightsColl = Core.Detection.ArcDetectionAll(Core.Detection.transform, data.lightRadius,
                data.lightAngle * 0.5f, data.starLightLayer, "Transform");
                
            // 星光转换
            foreach (var coll in _starLightsColl)
            {
                StarLight.StarLights[coll.GetInstanceID()].StarInFlashLight(data.transformTime);
            }

            // 伤害判定
            foreach (var coll in _monstersColl) 
                Monster.Monsters[coll.GetInstanceID()].MonsterEnterLight(data.lightDamage, true);
        }
        
        private bool LightOnMonster(Collider2D coll) => _monstersColl.Contains(coll);
        private bool LightOnStarLight(Collider2D coll) => _starLightsColl.Contains(coll);

        public void PlayerAudioClip(AudioClip clip)
        {
            _audio.clip = clip;
            _audio.Play();
        }
    }
}
