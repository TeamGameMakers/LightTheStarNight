using System;
using UnityEngine;

namespace Base.Mono
{
    /// <summary>
    /// Mono 承载类，提供 Update 和协程执行。
    /// 让不继承 MonoBehaviour 的类也可以使用 Update 和协程。
    /// </summary>
    public class MonoContainer : MonoBehaviour
    {
        protected event Action UpdateEvent;

        protected virtual void Update()
        {
            UpdateEvent?.Invoke();
        }

        /// <summary>
        /// 添加帧更新事件。
        /// </summary>
        /// <param name="action"></param>
        public virtual void AddUpdateListener(Action action)
        {
            UpdateEvent += action;
        }

        /// <summary>
        /// 移除帧更新事件。
        /// </summary>
        /// <param name="action"></param>
        public virtual void RemoveUpdateListener(Action action)
        {
            UpdateEvent -= action;
        }
    }
}

