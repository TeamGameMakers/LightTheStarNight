using System;

namespace Base.MVC
{
    public abstract class Controller
    {
        // 获取模型
        protected virtual Model GetModel(string name)
        {
            return MVC.GetModel(name);
        }

        protected virtual T GetModel<T>() where T : Model
        {
            return MVC.GetModel<T>();
        }

        // 获取视图
        protected virtual View GetView(string name)
        {
            return MVC.GetView(name);
        }

        protected virtual T GetView<T>() where T : View
        {
            return MVC.GetView<T>();
        }

        // 注册
        protected virtual void RegisterModel(Model model)
        {
            MVC.RegisterModel(model);
        }

        protected virtual void RegisterView(View view)
        {
            MVC.RegisterView(view);
        }

        protected virtual void RegisterController(string eventName, Type controllerType)
        {
            MVC.RegisterController(eventName, controllerType);
        }

        /// <summary>
        /// 接受事件，处理系统通知。
        /// </summary>
        /// <param name="data"></param>
        public abstract void Execute(object data);
    }
}
