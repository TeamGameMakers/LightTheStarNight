using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interact
{
    /// <summary>
    /// 会在一定范围内“漫步”的物体。
    /// </summary>
    public class RambleUiObject : MonoBehaviour
    {
        [Tooltip("漫步范围的长宽")]
        public Vector2 size = new Vector2(5, 5);

        [Tooltip("移速")]
        public float speed = 2;

        [Tooltip("停留时间")]
        public float parkTime = 0;

        protected RectTransform rTransform;
        
        // 点生成使用
        protected float minX, maxX, minY, maxY;
        
        // 移动标记
        protected bool moving = false;
        protected float curPark = 0;

        protected Vector2 startPos;

        // 移动参数
        protected Vector2 direction;
        protected float distance;

        protected virtual void Awake()
        {
            rTransform = transform as RectTransform;
            Vector2 pos = rTransform.anchoredPosition;
            float t = size.x / 2;
            minX = pos.x - t;
            maxX = pos.x + t;
            t = size.y / 2;
            minY = pos.y - t;
            maxY = pos.y + t;
        }

        protected virtual void Update()
        {
            // 检查是否要移动
            if (!moving && curPark >= parkTime)
            {
                curPark = 0;
                moving = true;
                
                // 生成点
                float tx = Random.Range(minX, maxX);
                float ty = Random.Range(minY, maxY);
                Vector2 targetPos = new Vector2(tx, ty);
                
                startPos = rTransform.anchoredPosition;
                // 计算方位
                Vector2 temp = targetPos - startPos;
                direction = temp.normalized;
                distance = temp.magnitude;
            }
            
            // 移动
            if (moving)
            {
                rTransform.anchoredPosition += direction * speed * Time.deltaTime;
                float dis = (rTransform.anchoredPosition - startPos).magnitude;
                if (dis >= distance)
                    moving = false;
            }
            else
            {
                curPark += Time.deltaTime;
            }
        }
    }
}
