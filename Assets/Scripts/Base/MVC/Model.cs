namespace Base.MVC
{
    public abstract class Model
    {
        public abstract string Name { get; }

        /// <summary>
        /// 向 Controller 和 View 通知数据更新。
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="data"></param>
        protected virtual void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }
    }
}
