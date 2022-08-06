using UnityEngine;

namespace Characters
{
    public class PlayerMoveState: PlayerState
    {
        private readonly int _animHashFloatX = Animator.StringToHash("velocityX");
        internal readonly int animHashFloatY = Animator.StringToHash("velocityY");
        
        public PlayerMoveState(Player player, string name) : base(player, name) { }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (InputHandler.NormInputX != 0)
            {
                _core.Movement.SetVelocityX(InputHandler.NormInputX * _data.speed);
                _core.Movement.SetVelocityY(0);
            }
            else if (InputHandler.NormInputY != 0)
            {
                _core.Movement.SetVelocityY(InputHandler.NormInputY * _data.speed);
                _core.Movement.SetVelocityX(0);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (InputHandler.RawMoveInput == Vector2.zero)
                StateMachine.ChangeState(_player.IdleState);
            else
            {
                _anim.SetFloat(_animHashFloatX, _core.Movement.CurrentVelocity.x);
                _anim.SetFloat(animHashFloatY, _core.Movement.CurrentVelocity.y);
            }
        }
    }
}