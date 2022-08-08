using Base;
using Core;
using Data;
using Interact;
using UnityEngine;

namespace Characters
{
    public class PlayerState : BaseState
    {
        private readonly int _animBoolHash;
    
        protected readonly Player _player;
        protected readonly Animator _anim;
        protected readonly GameCore _core;
        protected readonly PlayerDataSO _data;

        protected PlayerState(Player player, string name = null) : base(player.StateMachine)
        {
            _player = player;
            
            if (name != null)
                _animBoolHash = Animator.StringToHash(name);

            _anim = player.Anim;
            _core = player.Core;
            _data = player.data;
        }


        public override void Enter()
        {
            if (_animBoolHash != 0)
                _anim.SetBool(_animBoolHash, true);
        }

        public override void LogicUpdate()
        {
            if (!_player.isGrounded && !_player.isOnPlanet)
            {
                StateMachine.ChangeState(_player.DieState);
            }
        }

        public override void Exit()
        {
            if (_animBoolHash != 0)
                _anim.SetBool(_animBoolHash, false);
        }
    }
}
