using System;
using UnityEngine;

namespace Interact
{
    public class GroundCheck : MonoBehaviour
    {
        public static bool isGrounded;


        private void Start()
        {
            isGrounded = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                isGrounded = true;
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                isGrounded = false;
        }

    }
}
