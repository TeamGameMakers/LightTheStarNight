using Base.FSM;
using Core;
using Data;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerState : BaseState
    {
        private readonly int _animBoolHash;
    
        protected readonly Player _player;
        protected readonly Animator _anim;
        protected readonly GameCore _core;
        protected readonly PlayerDataSO _data;

        protected Vector2 InputVec2 { get; private set; }

        protected PlayerState(Player player, string name) : base(player.StateMachine)
        {
            this._player = player;
            _animBoolHash = Animator.StringToHash(name);

            _anim = player.Anim;
            _core = player.Core;
            _data = player.data;
        }


        public override void Enter()
        {
            _anim.SetBool(_animBoolHash, true);
        }

        public override void LogicUpdate()
        {
            InputVec2 = InputHandler.RawMoveInput;
        }

        public override void Exit()
        {
            _anim.SetBool(_animBoolHash, false);
        }
    }
}
