using Base.Scene;
using GM;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SuccessPanel : BasePanel
    {
        protected AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

        protected virtual void OnEnable()
        {
            GameManager.SwitchState(GameState.UI, false);
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "RestartBtn":
                    audioSource.Play();
                    // 重新加载场景
                    SceneLoader.LoadScene(SceneLoader.CurrentScene);
                    break;
                case "BackStartBtn":
                    audioSource.Play();
                    SceneLoader.LoadScene("开始界面");
                    break;
            }
        }

        protected virtual void OnDisable()
        {
            GameManager.SwitchState(GameState.Playing);
        }
    }
}