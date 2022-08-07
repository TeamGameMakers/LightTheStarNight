using System;
using TMPro;
using UnityEngine;

namespace Interact
{
    public class InteractableTip : MonoBehaviour
    {
        public TextMeshPro tmp;

        public float duration = 0.2f;

        protected float speed;
        protected bool fading = false;
        public bool Fading => fading;

        // 是否为逐渐出现
        protected bool fadeIn;

        public bool FadeInNow => fading && fadeIn;

        public bool FadeOutNow => fading && !fadeIn;
        
        // 是否已经出现
        protected bool appear = false;
        public bool Appear => appear;
        
        protected virtual void Awake()
        {
            if (tmp == null)
                tmp = GetComponent<TextMeshPro>();
            
            tmp.alpha = 0;
            speed = 1 / duration;
        }

        protected virtual void Update()
        {
            if (fading)
            {
                if (fadeIn)
                {
                    tmp.alpha += speed * Time.deltaTime;
                    if (tmp.alpha >= 0.95)
                    {
                        tmp.alpha = 1;
                        fading = false;
                        appear = true;
                    }
                }
                else
                {
                    tmp.alpha -= speed * Time.deltaTime;
                    if (tmp.alpha <= 0.05)
                    {
                        tmp.alpha = 0;
                        fading = false;
                    }
                }
            }
        }

        public virtual void Show()
        {
            fadeIn = true;
            fading = true;
        }

        public virtual void Hide()
        {
            fadeIn = false;
            fading = true;
            // 不再为完全出现
            appear = false;
        }
    }
}