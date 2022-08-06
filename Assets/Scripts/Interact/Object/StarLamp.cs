using System;
using Base;
using UnityEngine;

namespace Interact
{
    /// <summary>
    /// 能够被星光点亮，需要多次点亮。
    /// </summary>
    public class StarLamp : MonoBehaviour
    {
        public int needLighten = 3;
        
        private int m_lighten = 0;

        public const string LightenFullEvent = "StarLampLightenFullEvent";
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            // TODO: 检查进入的是星光
            if (true)
            {
                ++m_lighten;
                // TODO: 改变自身显示状态
                
                // 触发点亮事件
                EventCenter.Instance.EventTrigger(LightenFullEvent);
            }
        }
    }
}
