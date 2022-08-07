using UnityEngine;

namespace Interact
{
    public class RambleObject : MonoBehaviour
    {
        [Tooltip("漫步范围的长宽")]
        public Vector2 size = new Vector2(0.18f, 0.18f);

        [Tooltip("移速")]
        public float speed = 2;

        [Tooltip("停留时间")]
        public float parkTime = 0;
        
        // 点生成使用
        protected float minX, maxX, minY, maxY;
        
        // 移动标记
        protected bool moving = false;
        protected float curPark = 0;

        protected Vector3 startPos;

        // 移动参数
        protected Vector3 direction;
        protected float distance;
        
        // 需要保持为 true 才会颤动
        protected bool rambleOnce = false;
        
        protected virtual void Awake()
        {
            Vector3 pos = transform.position;
            float t = size.x / 2;
            minX = pos.x - t;
            maxX = pos.x + t;
            t = size.y / 2;
            minY = pos.y - t;
            maxY = pos.y + t;
        }
        
        protected virtual void Update()
        {
            if (rambleOnce)
            {
                // 检查是否要移动
                if (!moving && curPark >= parkTime)
                {
                    curPark = 0;
                    moving = true;
                
                    // 生成点
                    float tx = Random.Range(minX, maxX);
                    float ty = Random.Range(minY, maxY);
                    Vector3 targetPos = new Vector3(tx, ty);
                
                    startPos = transform.position;
                    // 计算方位
                    Vector3 temp = targetPos - startPos;
                    direction = temp.normalized;
                    distance = temp.magnitude;
                }
            
                // 移动
                if (moving)
                {
                    transform.position += direction * speed * Time.deltaTime;
                    float dis = (transform.position - startPos).magnitude;
                    if (dis >= distance)
                        moving = false;
                }
                else
                {
                    curPark += Time.deltaTime;
                }

                rambleOnce = false;
            }
        }

        // 保持调用才会抖动
        public void Shake()
        {
            Debug.Log("保持抖动");
            rambleOnce = true;
        }
    }
}