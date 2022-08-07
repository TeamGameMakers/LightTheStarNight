using System;
using UnityEngine;

namespace Interact
{
    public class LoopGameObject : MonoBehaviour
    {
        [Tooltip("开始点")]
        public Transform startPoint;
        protected Vector3 startPosition;
        [Tooltip("结束点")]
        public Transform endPoint;
        protected Vector3 endPosition;

        [Tooltip("移动速度")]
        public float speed = 10;

        [Tooltip("是否移动")]
        public bool movable = true;

        // 移动方向
        protected Vector3 direction;
        // 移动距离
        protected float distance;
        // 是否正在移动
        protected bool moving = false;

        protected void Awake()
        {
            startPosition = startPoint.position;
            endPosition = endPoint.position;

            Vector3 temp = endPosition - startPosition;
            direction = temp.normalized;
            distance = temp.magnitude;
        }

        protected virtual void Start()
        {
            transform.position = startPosition;
        }

        protected virtual void FixedUpdate()
        {
            // 可以移动才进入
            if (movable)
            {
                // 不在移动
                if (!moving)
                {
                    StayingBehaviour();
                }
                
                // 正在移动
                if (moving)
                {
                    MovingBehaviour();
                }
            }
        }

        protected virtual void StayingBehaviour()
        {
            ResumeMoving();
            moving = true;
        }

        protected virtual void MovingBehaviour()
        {
            MoveObject();
                    
            // 判断和起点的距离（position 是只读，不能预先存储为 Vector3）
            float tempDis = (transform.position - startPosition).magnitude;
            // 抵达终点
            if (tempDis >= distance)
            {
                ToEnd();
                moving = false;
            }
        }

        // 从停留恢复移动
        protected virtual void ResumeMoving()
        {
            transform.position = startPosition;
        }
        
        // 移动物体
        protected virtual void MoveObject()
        {
            transform.position += direction * speed * Time.fixedDeltaTime;
        }

        // 抵达终点后的行为
        protected virtual void ToEnd()
        {
            transform.position = endPosition;
        }
    }
}
