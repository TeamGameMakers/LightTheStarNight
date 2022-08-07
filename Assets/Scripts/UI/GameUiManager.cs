using System;
using Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class GameUiManager : SingletonMono<GameUiManager>
    {
        public SettingPanel settingPanel;
        public FailedPanel failedPanel;
        public SuccessPanel successPanel;

        /// <summary>
        /// 只能传入该 Manager 持有的 Panel.
        /// </summary>
        public void ShowPanel(BasePanel panel)
        {
            panel.gameObject.SetActive(true);
        }

        public void HidePanel(BasePanel panel)
        {
            panel.gameObject.SetActive(false);
        }

        private void Update()
        {
            // 检查 esc 打开 setting panel
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                ShowPanel(settingPanel);
            }
        }
    }
}