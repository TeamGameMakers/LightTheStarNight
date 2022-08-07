using System;
using Base.Scene;
using GM;
using UnityEngine.InputSystem;

namespace UI
{
    public class SettingPanel : BasePanel
    {
        protected virtual void OnEnable()
        {
            GameManager.SwitchState(GameState.UI);
        }

        protected virtual void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                gameObject.SetActive(false);
            }
        }
        
        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "BackBtn":
                    gameObject.SetActive(false);
                    break;
                case "BackStartBtn":
                    SceneLoader.LoadScene("开始界面");
                    break;
            }
        }

        protected virtual void OnDisable()
        {
            GameManager.ResumeState();
        }
    }
}