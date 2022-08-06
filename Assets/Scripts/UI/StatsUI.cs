using Base;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StatsUI : BasePanel
    {
        [SerializeField] private PlayerDataSO _data;

        private Image _powerRemaining;

        protected override void Awake()
        {
            base.Awake();

            Init();
        }

        private void Init()
        {
            _powerRemaining = GetControl<Image>("Remaining");
            EventCenter.Instance.AddEventListener<float, bool>("UseBatteryPower", UseBatteryPower);
        }

        private void Update()
        {
            RefreshUI();
        }

        private void OnDestroy()
        {
            EventCenter.Instance.RemoveEventListener<float, bool>("UseBatteryPower", UseBatteryPower);
        }

        private void RefreshUI()
        {
            //TODO: 4个格子
            _powerRemaining.fillAmount = _data.powerRemaining / _data.maxPower;
        }

        private bool UseBatteryPower(float speed)
        {
            _data.powerRemaining -= speed * Time.deltaTime;

            return _data.powerRemaining > 0;
        }
    }
}
