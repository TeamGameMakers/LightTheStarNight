using System;
using Base.Scene;
using GM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class SettingPanel : BasePanel
    {
        public AudioSource audioSource;
        
        protected virtual void OnEnable()
        {
            GameManager.SwitchState(GameState.UI, false);
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
                    audioSource.Play();
                    gameObject.SetActive(false);
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