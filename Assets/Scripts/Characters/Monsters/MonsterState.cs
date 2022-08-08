using Base;
using Core;
using Data;
using UnityEngine;

namespace Characters
{
    public class MonsterState : BaseState
    {
        protected readonly Monster _monster;
        protected readonly GameCore _core;
        protected readonly MonsterDataSO _data;
        private readonly int _animBoolHash;

        protected MonsterState(Monster monster, string name = null) : base(monster.StateMachine)
        {
            _monster = monster;
            _core = monster.Core;
            _data = monster.Data;
            
            if (name != null)
                _animBoolHash = Animator.StringToHash(name);
        }

        public override void Enter()
        {
            if (_animBoolHash != 0)
                _monster.SetAnimBool(_animBoolHash, true);
        }

        public override void PhysicsUpdate()
        {
            _monster.target = _core.Detection.CircleDetection(_core.Detection.transform, _data.checkRadius, _data.checkLayer);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_data.healthPoint > 0 && _monster.Hit)
                StateMachine.ChangeState(_monster.ChaseState);
            
            if (_data.healthPoint <= 0)
                StateMachine.ChangeState(_monster.DieState);
        }

        public override void Exit()
        {
            if (_animBoolHash != 0)
                _monster.SetAnimBool(_animBoolHash, false);
        }
    }
}