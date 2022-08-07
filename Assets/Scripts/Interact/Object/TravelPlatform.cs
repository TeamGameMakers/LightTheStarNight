using System;
using Characters;
using UnityEngine;

namespace Interact
{
    public class TravelPlatform : TravelGameObject
    {
        protected Player player;
        
        protected virtual void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("接触 Player");
                if (player == null)
                    player = other.gameObject.GetComponent<Player>();
                
                // TODO: 有待优化
                // 提供环境速度
                Vector2 envVelocity = direction * speed;
                if (movable && moving)
                    player.environmentVelocity += envVelocity;
            }
        }
    }
}