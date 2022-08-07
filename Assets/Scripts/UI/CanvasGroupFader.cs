using System;
using System.Collections;
using Base.Interface;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// 使用 UGUI 的 CanvasGroup 实现淡入淡出。
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupFader : MonoBehaviour, IFloatFader
    {
        protected CanvasGroup canvasGroup;

        protected bool isFading = false;
        public bool IsFading => isFading;

        public float Alpha {
            get => canvasGroup.alpha;
            set => canvasGroup.alpha = Mathf.Clamp01(value);
        }
        
        [Tooltip("渐变时长")]
        public float fadeDuration = 0.5f;

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null) {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        /// <summary>
        /// 使用 FadeCoroutine 协程实现的渐变。
        /// </summary>
        /// <param name="target">目标透明度</param>
        /// <param name="callback">结束后的回调函数</param>
        public void Fade(float target, Action<float> callback = null)
        {
            StartCoroutine(FadeCoroutine(target, callback));
        }

        public IEnumerator FadeCoroutine(float target, Action<float> callback)
        {
            yield return FadeCoroutine(target);
            callback?.Invoke(target);
        }

        public IEnumerator FadeCoroutine(float target)
        {
            isFading = true;
            // 阻挡射线
            canvasGroup.blocksRaycasts = true;

            // 计算速度
            float fadeSpeed = Mathf.Abs(canvasGroup.alpha - target) / fadeDuration;

            while (!Mathf.Approximately(canvasGroup.alpha, target)) {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, fadeSpeed * Time.deltaTime);
                yield return canvasGroup.alpha;
            }

            isFading = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
