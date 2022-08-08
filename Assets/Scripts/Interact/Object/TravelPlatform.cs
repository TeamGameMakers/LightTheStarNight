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
                GroundCheck.isGrounded = true;
                
                Debug.Log("Player 在移动平台上");
                if (player == null)
                    player = other.gameObject.GetComponent<Player>();
                
                // 提供环境速度
                Vector2 envVelocity = direction * speed;
                if (movable && moving)
                    player.environmentVelocity = envVelocity;
                else
                    player.environmentVelocity = Vector2.zero;
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player 离开移动平台");
                player.environmentVelocity = Vector2.zero;
            }
        }
    }
}