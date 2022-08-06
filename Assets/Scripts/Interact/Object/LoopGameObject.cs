using System;
using UnityEngine;

namespace Interact
{
    public class LoopGameObject : MonoBehaviour
    {
        public Transform startPoint;
        protected Vector3 startPosition;
        public Transform endPoint;
        protected Vector3 endPosition;

        public float speed = 10;

        public bool moving = true;

        protected Vector3 direction;
        protected float distance;

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

        protected virtual void Update()
        {
            if (moving)
            {
                // 移动位置
                transform.position += direction * speed * Time.deltaTime;
                // 判断和起点的距离（position 是只读，不能预先存储为 Vector3）
                float tempDis = (transform.position - startPosition).magnitude;
                // 超过终点，恢复位置
                if (tempDis >= distance)
                    transform.position = startPosition;
            }
        }
    }
}
