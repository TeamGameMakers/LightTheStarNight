using System;
using Save;
using UnityEngine;

namespace Interact
{
    public class StarLightTip : InteractableTip
    {
        public const string SaveKey = "FirstStarLight";

        public override void Show()
        {
            if (!SaveManager.GetBool(SaveKey)) {
                base.Show();
                if (Appear)
                    SaveManager.Register(SaveKey);
            }
        }

        public override void Hide()
        {
            if (tmp.alpha > 0)
                base.Hide();
        }
    }
}