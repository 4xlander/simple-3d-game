using UnityEngine;

namespace Game
{
    public class Gizmo : MonoBehaviour
    {
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private float _radius = 0.2f;

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}
