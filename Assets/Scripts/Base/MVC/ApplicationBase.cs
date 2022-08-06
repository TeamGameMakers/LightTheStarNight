using System;
using UnityEngine;

namespace Base.MVC
{
    public abstract class ApplicationBase<T> : SingletonMono<T> where T : MonoBehaviour
    {
        protected virtual void RegisterController(string eventName, Type controllerType)
        {
            MVC.RegisterController(eventName, controllerType);
        }

        protected virtual void SendEvent(string eventName)
        {
            MVC.SendEvent(eventName);
        }
    }
}
