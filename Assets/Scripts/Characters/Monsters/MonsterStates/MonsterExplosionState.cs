using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class MonsterExplosionState: MonsterState
    {
        private List<Collider2D> _hits;
        private bool _exploding;
        
        public MonsterExplosionState(Monster monster, string name = null) : base(monster, name) { }

        public override void Enter()
        {
            base.Enter();

            _hits = new List<Collider2D>();
            _core.AIMovement.StopMoving();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (_exploding && _monster.explosionCollider.enabled)
            {
                Physics2D.OverlapCollider(_monster.explosionCollider, _data.filter, _hits);
            }
            
            if (_hits.Count > 0)
            {
                GM.GameManager.GameOver();
                _monster.MonsterDie();
            }
        }

        public void Explode() => _exploding = true;
    }
}