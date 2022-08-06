using UnityEngine;

namespace Interact
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Transform _point;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _interactableMask;

        private readonly Collider2D[] _colliders = new Collider2D[3];
        private int _num;
        private Interactable _selected;

        private void Update()
        {
            _num = Physics2D.OverlapCircleNonAlloc(_point.position, _radius, _colliders, _interactableMask);

            if (_num > 0)
            {
                _selected = _colliders[0].GetComponent<Interactable>();

                _selected.ShowTip();

                if (InputHandler.InteractPressed)
                    _selected.Interact(this);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _num > 0 ? Color.green : Color.red;
            Gizmos.DrawWireSphere(_point.position, _radius);
        }
    }
}
