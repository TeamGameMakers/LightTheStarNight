using System.Collections;
using UnityEngine;

namespace Characters.Monsters
{
    public class MonsterDieState : MonsterState
    {
        private readonly SpriteRenderer _spriteRenderer;

        public MonsterDieState(Monster monster, string name = null) : base(monster, name)
        {
            _spriteRenderer = _monster.GetComponent<SpriteRenderer>();
        }

        public override void Enter()
        {
            base.Enter();
            _monster.StartCoroutine(MonsterFade(_data.fadeSpeed));
        }

        private IEnumerator MonsterFade(float fadeSpeed)
        {
            var fadeColor = Color.white;
            
            while (_spriteRenderer.color.a > 0)
            {
                fadeColor.a -= fadeSpeed * Time.deltaTime;
                _spriteRenderer.color = fadeColor;
                yield return null;
            }
            
            _monster.MonsterDie();
        }
    }
}