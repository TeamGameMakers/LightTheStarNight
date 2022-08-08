using Interact;
using UnityEngine;

namespace Characters
{
    public class PlayerDieState: PlayerState
    {
        public PlayerDieState(Player player, string name = null) : base(player, name) { }

        public override void Enter()
        {
            base.Enter();
            
            _core.Movement.SetVelocity(Vector2.zero);

            GM.GameManager.GameOver(_player.isGrounded ?  _data.explode : _data.fall);
        }
    }
}