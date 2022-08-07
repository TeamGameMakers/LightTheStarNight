using UnityEngine;

namespace Characters
{
    public class MonsterMoveState: MonsterState
    {
        private readonly int _animFloatHash;

        protected MonsterMoveState(Monster monster, string name = null) : base(monster, name)
        {
            _animFloatHash = Animator.StringToHash("velocityY");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_core.AIMovement.CurrentVelocity.y != 0)
                _monster.SetAnimFloat(_animFloatHash ,_core.AIMovement.CurrentVelocity.y);
        }
    }
}