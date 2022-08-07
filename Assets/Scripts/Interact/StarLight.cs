using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Base.Resource;
using UnityEngine;

namespace Interact
{
    public class StarLight: MonoBehaviour
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _renderer;
        private StarLightInteractable _interactable;
        private float _time;
        private Collider2D _coll;
        private bool _changingBack;
        
        public static readonly Dictionary<int, StarLight> StarLights = new();
        
        [SerializeField] private LayerMask _layer;
        [SerializeField] private bool _canChange;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _coll = GetComponent<Collider2D>();
            _interactable = GetComponentInChildren<StarLightInteractable>();
        }

        private void Start()
        {
            if (_canChange)
                StartCoroutine(ChangeBack(5));
        }

        private void OnEnable()
        {
            StarLights.Add(_coll.GetInstanceID(), this);
            EventCenter.Instance.AddEventListener<float>("StarInFlashLight", StarInFlashLight);
        }

        private void OnDisable()
        {
            StarLights.Remove(_coll.GetInstanceID());
        }

        private void FixedUpdate()
        {
            var hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.3f,
                0, Vector2.up, 2, _layer);

            _renderer.sortingLayerName = hit ? "CoverPlayer" : "CoverByPlayer";
        }

        private void Update()
        {
            if (_time != 0 && _canChange)
                StarOutFlashLight();
        }

        public void StarInFlashLight(float timer)
        {
            _time += Time.deltaTime;
            if (_time >= timer)
            {
                var go = ResourceLoader.Load<GameObject>("Prefabs/好陨石");
                go.transform.position = transform.position;
                Destroy(gameObject);
            }
        }
        
        private void StarOutFlashLight()
        {
            if (EventCenter.Instance.EventTrigger<Collider2D, bool>("LightOnStarLight", _coll)) return;
            _time = 0;

            if (!_changingBack) StartCoroutine(ChangeBack(5));
        }

        private IEnumerator ChangeBack(float timer)
        {
            _changingBack = true;
            
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;

                if (_time > 0 || _interactable.isAbsorbing)
                {
                    _changingBack = false;
                    yield break;
                }
            }

            var go = ResourceLoader.Load<GameObject>("Prefabs/坏陨石");
            go.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}