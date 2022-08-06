using Data;
using UnityEngine;

namespace Characters.Monsters
{
    public class MonsterChaseState: MonsterState
    {
        public MonsterChaseState(Monster monster, string name = null) : base(monster, name) { }

        public override void Enter()
        {
            base.Enter();

            if (_monster.Hit && _monster.HitByPlayer)
                _core.AIMovement.CurrentDestination = GM.GameManager.Player;
            else
                _core.AIMovement.CurrentDestination = _monster.target.transform;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (_monster.target)
                _core.Detection.LookAtTarget(_monster.target.transform);
            
            if (_data.monsterType != MonsterDataSO.MonsterType.Boss)
                _core.AIMovement.SetSpeed(_monster.Hit? _data.hitSpeed : _data.chaseSpeed);

            if (!_monster.target) StateMachine.ChangeState(_monster.IdleState);
            
        }
    }
}