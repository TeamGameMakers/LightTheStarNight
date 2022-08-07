using System;
using Base.Scene;
using UnityEditor;
using UnityEngine;

namespace UI
{
    public class StartPanel : BasePanel
    {
        public CanvasGroupFader fader;

        protected AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "StartBtn":
                    audioSource.Play();
                    // 切换到游戏场景
                    fader.Fade(1, f => {
                        // TODO: 修改场景名
                        SceneLoader.LoadScene("Test-AC");
                    });
                    break;
                case "QuitBtn":
                    audioSource.Play();
#if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                    break;
            }
        }
    }
}
