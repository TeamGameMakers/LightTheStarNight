using System;
using System.Collections.Generic;

namespace Base.MVC
{
    public static class MVC
    {
        // 存储 MVC
        // 名字 -- 模型
        private static readonly IDictionary<string, Model> models = new Dictionary<string, Model>();
        // 名字 -- 视图
        private static readonly IDictionary<string, View> views = new Dictionary<string, View>();
        // 事件名 -- 控制器
        private static readonly IDictionary<string, Type> commandMap = new Dictionary<string, Type>();

        // 注册
        public static void RegisterModel(Model model)
        {
            models[model.Name] = model;
        }

        public static void RegisterView(View view)
        {
            views[view.Name] = view;
        }

        public static void RegisterController(string eventName, Type controllerType)
        {
            commandMap[eventName] = controllerType;
        }

        // 获取
        public static Model GetModel(string name)
        {
            return models[name];
        }

        public static T GetModel<T>() where T : Model
        {
            
            foreach (var item in models.Values) {
                if (item is T model) {
                    return model;
                }
            }

            return null;
        }

        public static View GetView(string name)
        {
            return views[name];
        }

        public static T GetView<T>() where T : View
        {
            foreach (var item in views.Values) {
                if (item is T view) {
                    return view;
                }
            }
            return null;
        }

        /// <summary>
        /// 向 Controller 和 View 发送事件。
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="data"></param>
        public static void SendEvent(string eventName, object data = null)
        {
            SendToController(eventName, data);

            SendToView(eventName, data);
        }

        public static void SendToController(string eventName, object data = null)
        {
            // 控制器响应事件
            if (commandMap.ContainsKey(eventName)) {
                Type t = commandMap[eventName];
                Controller c = Activator.CreateInstance(t) as Controller;
                // 控制器执行
                c.Execute(data);
            }
        }

        public static void SendToView(string eventName, object data = null)
        {
            // 视图响应事件
            foreach (var item in views.Values) {
                if (item.attentionEvents.Contains(eventName)) {
                    item.HandleEvent(eventName, data);
                }
            }
        }
    }
}
