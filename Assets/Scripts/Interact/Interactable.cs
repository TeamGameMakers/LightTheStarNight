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
        // 记录拾取的键
        public string PickSaveKey => "pick_item_" + GetInstanceID();
        
        protected InteractableTip itrtTip;
        
        [SerializeField] protected InteractableDataSO _data;
        [SerializeField] private Transform _checkPoint;

        protected virtual void Awake()
        {
            itrtTip = transform.GetChild(0)?.GetComponent<InteractableTip>();
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            if (itrtTip != null && itrtTip.enabled && !itrtTip.FadeOutNow)
            {
                var coll = Physics2D.
                    OverlapCircle(_checkPoint.position, _data.checkRadius, _data.checkLayer);

                if (!coll) itrtTip.Hide();
            }
        }

        /// <summary>
        /// 可互动高亮提示
        /// </summary>
        public virtual void ShowTip()
        {
            if (itrtTip != null && !itrtTip.FadeInNow)
                itrtTip.Show();   
        }

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
