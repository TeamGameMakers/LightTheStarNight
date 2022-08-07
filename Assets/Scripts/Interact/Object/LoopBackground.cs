using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Interact
{
    /// <summary>
    /// 该类功能实现并不完善。
    /// 要满足需要，需要在场景设定中布置好图片的位置。
    /// </summary>
    public class LoopBackground : MonoBehaviour
    {
        public List<Transform> bgObjs = new List<Transform>();

        public float speed = 10;

        public bool movable = true;
        
        protected List<Vector3> originalPos = new List<Vector3>();
        protected Vector3 direction;
        protected float distance;

        private void Awake()
        {
            // 记录起始位置
            foreach (var obj in bgObjs)
            {
                originalPos.Add(obj.position);
            }
            // 以前两张图片为计算依据
            Vector3 temp = originalPos[1] - originalPos[0];
            direction = temp.normalized;
            distance = temp.magnitude;
        }

        private void FixedUpdate()
        {
            if (movable)
            {
                // 按方向移动所有图片
                foreach (var obj in bgObjs)
                {
                    obj.position += direction * speed * Time.fixedDeltaTime;
                }
                // 检查第一个物体的移动距离
                float tempDis = (bgObjs[0].position - originalPos[0]).magnitude;
                // 距离满足后，说明需要循环一张图了
                if (tempDis >= distance)
                {
                    // 将最后一个物体改为第一个物体
                    int last = bgObjs.Count - 1;
                    Transform temp = bgObjs[last];
                    bgObjs.RemoveAt(last);
                    bgObjs.Insert(0, temp);
                    // 规格化所有物体的位置
                    for (int i = 0; i <= last; ++i)
                    {
                        bgObjs[i].position = originalPos[i];
                    }
                }
            }
        }
    }
}
