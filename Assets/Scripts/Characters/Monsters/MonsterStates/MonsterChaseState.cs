using UnityEngine;

namespace Characters
{
    public class MonsterChaseState: MonsterMoveState
    {
        private Transform _target;
        
        public MonsterChaseState(Monster monster, string name = null) : base(monster, name) { }

        public override void Enter()
        {
            base.Enter();

            _target = _monster.HitByPlayer ? GM.GameManager.Player : _monster.target.transform;
            _core.AIMovement.CurrentDestination = _target;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _core.AIMovement.SetSpeed(_monster.Hit? _data.hitSpeed : _data.chaseSpeed);

            if (_monster.target || _monster.HitByPlayer)
            {
                _core.Detection.LookAtTarget(_target);
                if (_monster.target)
                    _target = _monster.target.transform;
            }

            if (Vector3.Distance(_monster.transform.position, _target.position) <= _data.explosionDistance)
                StateMachine.ChangeState(_monster.ExplosionState);
            
            else if (!_monster.target && !_monster.HitByPlayer) 
                StateMachine.ChangeState(_monster.IdleState);
        }

        public override void Exit()
        {
            base.Exit();
            
            _core.AIMovement.SetSpeed(_data.chaseSpeed);
        }
    }
}