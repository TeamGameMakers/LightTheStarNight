using System;
using UnityEngine;

namespace Base.Event
{
    public abstract class EventRegister : MonoBehaviour
    {
        public string registerName;

        protected virtual void Awake()
        {
            EventCenter.Instance.AddEventListener(registerName, EventAction);
        }

        protected virtual void OnDisable()
        {
            EventCenter.Instance.RemoveEventListener(registerName, EventAction);
        }

        protected abstract void EventAction();
    }
}
