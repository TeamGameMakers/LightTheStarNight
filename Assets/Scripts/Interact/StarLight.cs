using System;
using Characters;
using UnityEngine;

namespace Interact
{
    public class StarLight: MonoBehaviour
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _renderer;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private bool _canChange;
 
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            var hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.3f,
                0, Vector2.up, 2, _layer);

            _renderer.sortingLayerName = hit ? "CoverPlayer" : "CoverByPlayer";
        }
    }
}