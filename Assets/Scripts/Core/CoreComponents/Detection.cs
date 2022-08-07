using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Core
{
    public class Detection : CoreComponent
    {
        [SerializeField] private int maxDetectNum = 10;
        private Collider2D[] _objects;
        private int _num;

        private void Awake()
        {
            _objects = new Collider2D[maxDetectNum];
        }

        public Collider2D[] CircleDetection(Transform origin, float radius, LayerMask layer, out int num)
        {
            _num = Physics2D.OverlapCircleNonAlloc(origin.position, radius, _objects, layer);
            num = _num;
            return _objects;
        }
        
        public Collider2D CircleDetection(Transform origin, float radius, LayerMask layer) 
            => Physics2D.OverlapCircle(origin.position, radius, layer);

        /// <summary>
        /// 扇形检测
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="radius"></param>
        /// <param name="angle">扇形角度(半角)</param>
        /// <param name="layer"></param>
        /// <param name="compareTag"></param>
        /// <returns></returns>
        public List<Collider2D> ArcDetectionAll(Transform origin, float radius, float angle, LayerMask layer, string compareTag = "Untagged")
        {
            List<Collider2D> result = new List<Collider2D>();
            
            CircleDetection(origin, radius, layer, out _num);
            
            for (int i = 0; i < _num; i++)
            {
                if (Utils.IsInArcSector(transform.right, 
                        _objects[i].transform.position - transform.position, angle)
                    && _objects[i].CompareTag(compareTag))
                    result.Add(_objects[i]);
            }

            return result;
        }

        public Collider2D ArcDetection(Transform origin, float radius, float angle, LayerMask layer)
        {
            var coll = CircleDetection(origin, radius, layer);

            if (!coll || !Utils.IsInArcSector(transform.right, 
                    coll.transform.position - transform.position, angle)) 
                coll = null;
            
            return coll;
        }

        public void LookAtTarget(Transform target) 
            => transform.right = (target.position - transform.position).normalized;

        public void LookAtTarget(Vector3 direction) => transform.right = direction;
        

    }
}