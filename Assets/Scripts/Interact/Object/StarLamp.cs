using System;
using Base;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private Animator m_animator;
        private static readonly int Brighten = Animator.StringToHash("Brighten");

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("StarLight"))
            {
                ++m_lighten;
                // 改变自身显示状态
                m_animator.SetTrigger(Brighten);
                // 销毁
                Destroy(col.gameObject);
                
                // 完全亮起，触发点亮事件
                if (m_lighten == needLighten)
                    EventCenter.Instance.EventTrigger(LightenFullEvent);
            }
        }
    }
}
