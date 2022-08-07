using System;
using System.Collections.Generic;
using Base;
using GM;
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

        private int m_brightenCount = 0;

        private void Start()
        {
            fader.gameObject.SetActive(true);
            GameManager.SwitchState(GameState.UI);
            fader.Fade(0, f => {
                GameManager.SwitchState(GameState.Playing);
                Destroy(fader.gameObject);
            });
            
            // 注册增加点亮灯塔的事件
            EventCenter.Instance.AddEventListener(LightenFullEvent, () => ++m_brightenCount);
        }

        private void Update()
        {
            if (m_brightenCount == 4)
            {
                foreach (var obj in brightenObjs)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
