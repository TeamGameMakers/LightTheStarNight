using System;
using System.Collections.Generic;
using Base;
using GM;
using TMPro;
using UI;
using UnityEngine;

namespace Interact
{
    /// <summary>
    /// 游戏场景开始的一些处理。
    /// 渐隐；
    /// 播放 BGM;
    /// 切换游戏状态；
    /// 检查亮灯数。
    /// </summary>
    public class GameSceneManager : MonoBehaviour
    {
        public CanvasGroupFader fader;
        
        public const string LightenFullEvent = "StarLampLightenFullEvent";

        public List<GameObject> brightenObjs = new List<GameObject>();

        public TextMeshProUGUI countTmp;
        
        [SerializeField]
        [Tooltip("点亮的灯塔数")]
        private int m_brightenCount = 0;

        public AudioClip brightenClip; 
            
        private AudioSource m_audioSource;
        private bool changedMusic = false;

        private void Awake()
        {
            m_audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            fader.gameObject.SetActive(true);
            GameManager.SwitchState(GameState.UI);
            fader.Fade(0, f => {
                GameManager.SwitchState(GameState.Playing);
                Destroy(fader.gameObject);
            });
            
            // 注册增加点亮灯塔的事件
            EventCenter.Instance.AddEventListener(LightenFullEvent, CountBrighten);
        }

        private void Update()
        {
            if (m_brightenCount == 4)
            {
                foreach (var obj in brightenObjs)
                {
                    obj.SetActive(true);
                }

                if (!changedMusic)
                {
                    changedMusic = true;
                    m_audioSource.clip = brightenClip;
                    m_audioSource.Play();
                }
            }
            else if (m_brightenCount == 5)
            {
                // 弹出通关画面
                GameUiManager.Instance.ShowPanel(GameUiManager.Instance.successPanel);
            }
        }

        private void CountBrighten()
        {
            ++m_brightenCount;
            countTmp.SetText(m_brightenCount.ToString());
        }

        private void ChangeBgm()
        {
            // 更换背景音乐
            m_audioSource.clip = brightenClip;
            m_audioSource.Play();
        }
    }
}
