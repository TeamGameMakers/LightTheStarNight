using Utilities;

namespace Characters
{
    public class MonsterReturnState: MonsterState
    {
        public MonsterReturnState(Monster monster, string name = null) : base(monster, name) { }

        public override void Enter()
        {
            base.Enter();
            
            _core.AIMovement.CurrentDestination = _monster.SpawnTransform;
            _core.AIMovement.SetSpeed(_data.walkSpeed);
            _core.Detection.LookAtTarget(_monster.SpawnTransform);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (Utils.IsArriveAtDestination(_monster.transform, _monster.SpawnTransform, 0.01f))
                StateMachine.ChangeState(_monster.IdleState);
        }

        public override void Exit()
        {
            base.Exit();
            
            _core.Detection.LookAtTarget(_monster.SpawnTransform.right);
        }
    }
}