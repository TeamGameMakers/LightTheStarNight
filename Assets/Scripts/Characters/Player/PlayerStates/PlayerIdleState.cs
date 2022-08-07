using UnityEngine;

namespace Characters
{
    public sealed class PlayerIdleState: PlayerState
    {
        public PlayerIdleState(Player player, string name) : base(player, name) { }

        public override void Enter()
        {
            base.Enter();
            _core.Movement.SetVelocity(Vector2.zero);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (InputHandler.RawMoveInput != Vector2.zero) 
                StateMachine.ChangeState(_player.MoveState);
        }
    }
}