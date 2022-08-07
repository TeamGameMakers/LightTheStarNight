using System;
using Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BatteryPanel : BasePanel
    {
        [SerializeField] private Sprite[] _sprites;

        private Image _image;

        protected override void Awake()
        {
            base.Awake();

            _image = GetControl<Image>("Image");
        }

        protected void OnEnable()
        {
            EventCenter.Instance.AddEventListener<int>("UpdateBatteryUI", UpdateBatteryUI);
        }

        protected void OnDisable()
        {
            EventCenter.Instance.RemoveEventListener<int>("UpdateBatteryUI", UpdateBatteryUI);
        }

        private void UpdateBatteryUI(int powerUsed)
        {
            _image.enabled = powerUsed <= 3;

            if (powerUsed > 3) return;
            
            _image.sprite = _sprites[powerUsed];
        }
    }
}
