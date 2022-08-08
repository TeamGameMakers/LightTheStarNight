using System;
using UnityEngine;

namespace Interact
{
    /// <summary>
    /// 进入触发器范围就提高 Sprite 的 Sorting Layer
    /// </summary>
    public class LayerUpperObject : MonoBehaviour
    {
        [Tooltip("会自动获取第一个 parent 的 renderer")]
        public new SpriteRenderer renderer;

        public string defaultSortingLayer = "CoverByPlayer";

        public string targetSortingLayer = "CoverPlayer";

        public LayerMask coverLayer;

        protected virtual void Awake()
        {
            if (renderer == null)
                renderer = transform.parent.GetComponent<SpriteRenderer>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (col.IsTouchingLayers(1 << coverLayer))
                renderer.sortingLayerName = targetSortingLayer;
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (other.IsTouchingLayers(1 << coverLayer))
                renderer.sortingLayerName = defaultSortingLayer;
        }
    }
}
