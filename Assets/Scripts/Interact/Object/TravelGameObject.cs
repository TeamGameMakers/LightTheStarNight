using UnityEngine;

namespace Interact
{
    public class TravelGameObject : LoopGameObject
    {
        [Tooltip("停留时间")]
        public float parkTime = 2;

        // 已停留时间
        protected float currentPark = 0;

        protected override void StayingBehaviour()
        {
            if (!Parking())
            {
                base.StayingBehaviour();
            }
        }

        protected override void ResumeMoving()
        {
            currentPark = 0;
        }

        protected override void ToEnd()
        {
            base.ToEnd();
            // 交换起点和终点
            (startPosition, endPosition) = (endPosition, startPosition);
            // 更改方向
            direction = -direction;
        }

        /// <summary>
        /// 记录停留时间。
        /// </summary>
        /// <returns>是否还在停留</returns>
        protected virtual bool Parking()
        {
            currentPark += Time.fixedDeltaTime;
            if (currentPark >= parkTime)
                return false;
            return true;
        }
    }
}