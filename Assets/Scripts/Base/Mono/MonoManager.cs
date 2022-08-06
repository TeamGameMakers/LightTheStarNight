using System;
using System.Collections;
using UnityEngine;

namespace Base.Mono
{
    /// <summary>
    /// 提供非 MonoBehaviour 子类使用 Update 和协程的功能。
    /// </summary>
    public class MonoManager : Singleton<MonoManager>
    {
        // Mono 承载对象名称
        public string Name = "MonoContainer";

        protected readonly MonoContainer container;

        public MonoManager()
        {
            GameObject obj = new GameObject(Name);
            container = obj.AddComponent<MonoContainer>();
            GameObject.DontDestroyOnLoad(obj);
        }

        /// <summary>
        /// 添加帧更新事件。
        /// </summary>
        /// <param name="action"></param>
        public virtual void AddUpdateListener(Action action)
        {
            container.AddUpdateListener(action);
        }

        /// <summary>
        /// 移除帧更新事件。
        /// </summary>
        /// <param name="action"></param>
        public virtual void RemoveUpdateListener(Action action)
        {
            container.RemoveUpdateListener(action);
        }

        public virtual Coroutine StartCoroutine(IEnumerator routine)
        {
            return container.StartCoroutine(routine);
        }

        public virtual void StopCoroutine(IEnumerator routine)
        {
            container.StopCoroutine(routine);
        }

        public virtual void StopCoroutine(Coroutine routine)
        {
            container.StopCoroutine(routine);
        }
    }
}
