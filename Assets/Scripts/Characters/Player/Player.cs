using Base.FSM;
using Core;
using Data;
using UnityEngine;

namespace Characters.Player
{
    public class Player : Entity
    {
        internal PlayerStateMachine StateMachine { get; private set; }
        internal Animator Anim { get; private set; }
        internal InputHandler InputHandler { get; private set; }
        internal GameCore Core { get; private set; }
        
        public PlayerDataSO data;

        #region States
    
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
    
        #endregion

        private void Awake()
        {
            Anim = GetComponent<Animator>();
            InputHandler = GetComponent<InputHandler>();
            Core = GetComponentInChildren<GameCore>();

            StateMachine = new PlayerStateMachine();
            IdleState = new PlayerIdleState(this, "idle");
            MoveState = new PlayerMoveState(this, "move");
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
            GM.GameManager.SetPlayerTransform(transform);
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void Update()
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();
        }
        
        /// <summary>
        /// 进入光
        /// </summary>
        public void PlayerEnterLight(int damage)
        {
            Debug.Log("玩家进入光");
        }

        /// <summary>
        /// 离开光
        /// </summary>
        public void PlayerExitLight()
        {
            Debug.Log("玩家离开光");
        }
    }
}
