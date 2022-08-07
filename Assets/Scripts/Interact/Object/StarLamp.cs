using System;
using Base;
using Base.Resource;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

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

        private AudioSource m_audioSource;

        private bool m_readyPlayLightClip = false;

        public new Light2D light;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_audioSource = GetComponent<AudioSource>();
            if (m_audioSource == null)
            {
                m_audioSource = gameObject.AddComponent<AudioSource>();
                m_audioSource.playOnAwake = false;
                m_audioSource.loop = false;
            }
        }

        private void Update()
        {
            // if (Mouse.current.leftButton.wasPressedThisFrame)
            // {
            //     ++m_lighten;
            //     // 触发音效
            //     switch (m_lighten)
            //     {
            //         case 1:
            //             m_audioSource.clip = ResourceLoader.Load<AudioClip>("Audio/starlight1");
            //             m_audioSource.Play();
            //             break;
            //         case 2:
            //             m_audioSource.clip = ResourceLoader.Load<AudioClip>("Audio/starlight2");
            //             m_audioSource.Play();
            //             break;
            //         case 3:
            //             m_audioSource.clip = ResourceLoader.Load<AudioClip>("Audio/starlight3");
            //             m_audioSource.Play();
            //             m_readyPlayLightClip = true;
            //             break;
            //     }
            //     // 改变自身显示状态
            //     m_animator.SetTrigger(Brighten);
            //     
            //     // 完全亮起，触发点亮事件
            //     if (m_lighten == needLighten)
            //     {
            //         EventCenter.Instance.EventTrigger(LightenFullEvent);
            //     }
            // }
            
            // 检查播放最终音效
            if (m_readyPlayLightClip && m_audioSource.time >= 1.3)
            {
                m_audioSource.clip = ResourceLoader.Load<AudioClip>("Audio/starlightACT");
                m_audioSource.Play();
                m_readyPlayLightClip = false;
                light.gameObject.SetActive(true);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("StarLight") && m_lighten < needLighten)
            {
                ++m_lighten;
                // 触发音效
                switch (m_lighten)
                {
                    case 1:
                        m_audioSource.clip = ResourceLoader.Load<AudioClip>("Audio/starlight1");
                        m_audioSource.Play();
                        break;
                    case 2:
                        m_audioSource.clip = ResourceLoader.Load<AudioClip>("Audio/starlight2");
                        m_audioSource.Play();
                        break;
                    case 3:
                        m_audioSource.clip = ResourceLoader.Load<AudioClip>("Audio/starlight3");
                        m_audioSource.Play();
                        m_readyPlayLightClip = true;
                        break;
                }
                // 改变自身显示状态
                m_animator.SetTrigger(Brighten);
                // 销毁
                Destroy(col.transform.parent.gameObject);
                
                // 完全亮起，触发点亮事件
                if (m_lighten == needLighten)
                    EventCenter.Instance.EventTrigger(LightenFullEvent);
            }
        }
    }
}
