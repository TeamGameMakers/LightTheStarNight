using System;
using System.Collections.Generic;

namespace Base
{
    /// <summary>
    /// 事件中心。
    /// </summary>
    public class EventCenter : Singleton<EventCenter>
    {
        protected readonly Dictionary<string, IEvent> eventContainer = new Dictionary<string, IEvent>();

        /// <summary>
        /// 添加无参无返回值事件监听。
        /// </summary>
        /// <param name="name">事件名</param>
        /// <param name="callback">用来处理事件的函数</param>
        public void AddEventListener(string name, Action callback)
        {
            if (eventContainer.ContainsKey(name)) {
                (eventContainer[name] as EventAction).actions += callback;
            }
            else {
                eventContainer.Add(name, new EventAction(callback));
            }
        }

        /// <summary>
        /// 添加一个参数无返回值事件监听。
        /// </summary>
        /// <typeparam name="T">回调函数参数类型</typeparam>
        /// <param name="name">事件名</param>
        /// <param name="callback">用来处理事件的函数</param>
        public void AddEventListener<T>(string name, Action<T> callback)
        {
            if (eventContainer.ContainsKey(name)) {
                (eventContainer[name] as EventAction<T>).actions += callback;
            }
            else {
                eventContainer.Add(name, new EventAction<T>(callback));
            }
        }

        public void AddEventListener<T>(string name, Func<T> callback)
        {
            if (eventContainer.ContainsKey(name))
                (eventContainer[name] as EventFunc<T>).funcs += callback;
            else
                eventContainer.Add(name, new EventFunc<T>(callback));
        }

        public void AddEventListener<T1, T2>(string name, Func<T1, T2> callback)
        {
            if (eventContainer.ContainsKey(name))
                (eventContainer[name] as EventFunc<T1, T2>).funcs += callback;
            else
                eventContainer.Add(name, new EventFunc<T1, T2>(callback));
        }

        /// <summary>
        /// 移除事件监听。
        /// </summary>
        /// <param name="name">事件名</param>
        /// <param name="callback">对应之前添加的函数</param>
        public void RemoveEventListener(string name, Action callback)
        {
            if (eventContainer.ContainsKey(name)) {
                (eventContainer[name] as EventAction).actions -= callback;
            }
        }

        /// <summary>
        /// 移除事件监听。
        /// </summary>
        /// <typeparam name="T">回调函数参数类型</typeparam>
        /// <param name="name">事件名</param>
        /// <param name="callback">对应之前添加的函数</param>
        public void RemoveEventListener<T>(string name, Action<T> callback)
        {
            if (eventContainer.ContainsKey(name)) {
                (eventContainer[name] as EventAction<T>).actions -= callback;
            }
        }

        public void RemoveEventListener<T1, T2>(string name, Func<T1, T2> callback)
        {
            if (eventContainer.ContainsKey(name))
                (eventContainer[name] as EventFunc<T1, T2>).funcs -= callback;
        }

        /// <summary>
        /// 事件触发。
        /// </summary>
        /// <param name="name">触发的事件名</param>
        public void EventTrigger(string name)
        {
            if (eventContainer.ContainsKey(name)) {
                (eventContainer[name] as EventAction).actions?.Invoke();
            }
        }

        /// <summary>
        /// 事件触发。
        /// </summary>
        /// <typeparam name="T">回调函数参数类型</typeparam>
        /// <param name="name">触发的事件名</param>
        /// <param name="info">回调函数参数</param>
        public void EventTrigger<T>(string name, T info)
        {
            if (eventContainer.ContainsKey(name)) {
                (eventContainer[name] as EventAction<T>).actions?.Invoke(info);
            }
        }

        public T EventTrigger<T>(string name)
        {
            if (eventContainer.ContainsKey(name))
                return (eventContainer[name] as EventFunc<T>).funcs.Invoke();
            
            return default(T);
        }

        public T2 EventTrigger<T1, T2>(string name, T1 value)
        {
            if (eventContainer.ContainsKey(name))
                return (eventContainer[name] as EventFunc<T1, T2>).funcs.Invoke(value);
            
            return default(T2);
        }

        /// <summary>
        /// 清空事件中心。
        /// 主要在场景切换时。
        /// </summary>
        public void Clear()
        {
            eventContainer.Clear();
        }



        #region 内部类
        
        protected interface IEvent {}

        /// <summary>
        /// 用于给字典装载不同 Action 的识别接口。
        /// </summary>
        protected interface IEventAction: IEvent { }
        
        protected interface IEventFunc: IEvent { }

        /// <summary>
        /// 无参无返回值委托。
        /// </summary>
        protected class EventAction : IEventAction
        {
            public Action actions;

            public EventAction(Action action)
            {
                actions += action;
            }

            public static implicit operator Action(EventAction ea)
            {
                return ea.actions;
            }
        }

        /// <summary>
        /// 一个参数无返回值委托。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected class EventAction<T> : IEventAction
        {
            public Action<T> actions;

            public EventAction(Action<T> action)
            {
                actions += action;
            }

            public static implicit operator Action<T>(EventAction<T> ea)
            {
                return ea.actions;
            }
        }
        
        protected class EventFunc<T> : IEventFunc
        {
            public Func<T> funcs;

            public EventFunc(Func<T> func) => funcs += func;
            
            public static implicit operator Func<T>(EventFunc<T> ef) => ef.funcs;
        }

        protected class EventFunc<T1, T2> : IEventFunc
        {
            public Func<T1, T2> funcs;

            public EventFunc(Func<T1, T2> func) => funcs += func;

            public static implicit operator Func<T1, T2>(EventFunc<T1, T2> ef) => ef.funcs;
        }
        
        #endregion
    }
}
