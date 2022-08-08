using System;
using System.ComponentModel;
using Base.Scene;
using Characters;
using GM;
using Interact;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FailedPanel : BasePanel
    {
        public AudioSource audioSource;
        protected RawImage image;

        protected override void Awake()
        {
            base.Awake();
            image = transform.Find("Image").GetComponent<RawImage>();
        }

        public void ChangeImage(Texture sprite)
        {
            image.texture = sprite;
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
                case "ContinueBtn":
                    audioSource.Play();
                    // 重置玩家位置
                    GameManager.ResetPlayer();
                    // GroundCheck.isGrounded = true;

                    GameManager.player.GetComponent<Player>().isGrounded = true;
                    gameObject.SetActive(false);
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
            GameManager.SwitchState(GameState.Playing);
        }
    }
}
