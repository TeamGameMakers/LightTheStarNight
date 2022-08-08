using System;
using UnityEngine;

namespace Interact
{
    /// <summary>
    /// 进入触发器范围就提高 Sprite 的 Sorting Layer
    /// </summary>
    public class LayerUpperObject : MonoBehaviour
    {
        public SpriteRenderer renderer;

        public string defaultSortingLayer = "CoverByPlayer";

        public string targetSortingLayer = "CoverPlayer";
        
        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            renderer.sortingLayerName = targetSortingLayer;
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            renderer.sortingLayerName = defaultSortingLayer;
        }
    }
}
