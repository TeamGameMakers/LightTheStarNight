using UnityEngine;

namespace Characters
{
    public class MonsterExplosionState: MonsterState
    {
        private AnimationEvent _explode;
        
        public MonsterExplosionState(Monster monster, string name = null) : base(monster, name) { }

        public override void Enter()
        {
            base.Enter();
            
            _core.AIMovement.StopMoving();
        }
    }
}