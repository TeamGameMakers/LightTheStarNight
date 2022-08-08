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

        public Button settingBtn;
        private AudioSource m_audioSource;

        protected override void Awake()
        {
            base.Awake();

            _image = GetControl<Image>("Image");

            m_audioSource = GetComponent<AudioSource>();
        }

        protected virtual void Start()
        {
            settingBtn.onClick.AddListener( () => {
                m_audioSource.Play();
                GameUiManager.Instance.ShowPanel(GameUiManager.Instance.settingPanel);
            });
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
