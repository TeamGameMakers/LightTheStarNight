using Base.Event;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Interact.Scene
{
    public class StarLampLightenRegister : EventRegister
    {
        public Light2D globalLight;

        public float originalIntensity = 0;

        public float maxIntensity = 1;

        public int maxRank = 5;

        public float duration = 0.3f;

        // 当前亮度
        protected float curIntensity;
        // 每级变亮的程度
        protected float intensityPerRank;
        // 变亮的速度
        protected float speed;

        protected bool brightening = false;
        
        protected virtual void Start()
        {
            globalLight.intensity = originalIntensity;
            intensityPerRank = (maxIntensity - originalIntensity) / maxRank;
            speed = intensityPerRank / duration;
            curIntensity = originalIntensity;
        }

        private void Update()
        {
            // 逐渐变亮
            if (brightening)
            {
                globalLight.intensity += speed * Time.deltaTime;
                if (globalLight.intensity >= curIntensity)
                {
                    globalLight.intensity = curIntensity;
                    brightening = false;
                }
            }
        }

        protected override void EventAction()
        {
            // 每次触发都变亮一个级别
            curIntensity += intensityPerRank;
            brightening = true;
        }
    }
}