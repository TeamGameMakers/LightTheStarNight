using System;
using GM;
using UI;
using UnityEngine;

namespace Interact
{
    /// <summary>
    /// 游戏场景开始的一些处理。
    /// 渐隐；
    /// 播放 BGM;
    /// 切换游戏状态
    /// </summary>
    public class GameSceneManager : MonoBehaviour
    {
        public CanvasGroupFader fader;

        private void Start()
        {
            fader.gameObject.SetActive(true);
            GameManager.SwitchState(GameState.UI);
            fader.Fade(0, f => {
                GameManager.SwitchState(GameState.Playing);
                Destroy(fader.gameObject);
            });
        }
    }
}
