using UnityEditor;
using UnityEngine;

namespace UI
{
    public class StartPanel : BasePanel
    {
        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "StartBtn":
                    // 切换到游戏场景
                    
                    break;
                case "QuitBtn":
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
