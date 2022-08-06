using System.Collections.Generic;
using UnityEngine;

namespace Base.MVC
{
    public abstract class View : MonoBehaviour
    {
        public abstract string Name { get; }

        [Tooltip("该视图关心的事件")]
        public IList<string> attentionEvents = new List<string>();

        /// <summary>
        /// 主要由 Controller 接受事件后，调用处理。
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="data"></param>
        public abstract void HandleEvent(string eventName, object data);

        // 获取模型
        protected virtual Model GetModel(string name)
        {
            return MVC.GetModel(name);
        }

        protected virtual T GetModel<T>() where T : Model
        {
            return MVC.GetModel<T>();
        }

        /// <summary>
        /// 向其他 Controller 和 View 发送通知。
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="data"></param>
        protected virtual void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }
    }
}
