using System;
using System.ComponentModel;
using Base.Scene;
using GM;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FailedPanel : BasePanel
    {
        protected AudioSource audioSource;
        protected RawImage image;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
            image = transform.Find("Image").GetComponent<RawImage>();
            // TODO: 根据失败清空不同，设置不同的图片。
        }

        protected virtual void OnEnable()
        {
            GameManager.SwitchState(GameState.UI);
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "ContinueBtn":
                    audioSource.Play();
                    // 重新加载场景
                    SceneLoader.LoadScene(SceneLoader.CurrentScene);
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

        protected virtual void OnDisable()
        {
            GameManager.ResumeState();
        }
    }
}
