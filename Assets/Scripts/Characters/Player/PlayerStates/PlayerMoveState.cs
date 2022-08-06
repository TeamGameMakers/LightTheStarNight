using UnityEngine;

namespace Characters.Player
{
    public class PlayerMoveState: PlayerState
    {
        private readonly int _animHashFloatX = Animator.StringToHash("velocityX");
        private readonly int _animHashFloatY = Animator.StringToHash("velocityY");
        
        public PlayerMoveState(Characters.Player.Player player, string name) : base(player, name) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            var curVelocity = InputHandler.SprintPressed ? _data.runVelocity : _data.walkVelocity;
            _core.Movement.SetVelocity(InputVec2 * curVelocity);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (InputVec2 == Vector2.zero)
                StateMachine.ChangeState(_player.IdleState);
            else
            {
                _anim.SetFloat(_animHashFloatX, InputHandler.NormInputX);
                _anim.SetFloat(_animHashFloatY, InputHandler.NormInputY);
            }
        }
    }
}