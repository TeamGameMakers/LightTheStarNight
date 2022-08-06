using System;
using System.Collections;

namespace Base.Interface
{
    public interface IFloatFader
    {
        public bool IsFading { get; }

        public void Fade(float target, Action<float> callback);

        public IEnumerator FadeCoroutine(float target);
    }
}
