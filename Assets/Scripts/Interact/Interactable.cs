using System;
using Data;
using UnityEngine;

namespace Interact
{
    /// <summary>
    /// 交互接口
    /// </summary>
    public abstract class Interactable: MonoBehaviour
    {
        protected SpriteRenderer _spriteRenderer;
        
        [SerializeField] protected InteractableDataSO _data;
        [SerializeField] private Transform _checkPoint;

        protected virtual void Awake()
        {
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        protected virtual void Start()
        {
            _spriteRenderer.enabled = false;
        }

        protected virtual void Update()
        {
            if (_spriteRenderer.enabled)
            {
                var coll = Physics2D.
                    OverlapCircle(_checkPoint.position, _data.checkRadius, _data.checkLayer);

                if (!coll) _spriteRenderer.enabled = false;
            }
        }

        /// <summary>
        /// 可互动高亮提示
        /// </summary>
        public virtual void ShowTip() => _spriteRenderer.enabled = true;

        /// <summary>
        /// 交互的抽象方法
        /// </summary>
        /// <param name="interactor"></param>
        public abstract void Interact(Interactor interactor);
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_checkPoint.position, _data.checkRadius);
        }
    }
}
