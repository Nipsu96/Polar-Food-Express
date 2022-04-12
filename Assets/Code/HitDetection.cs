using UnityEngine;

namespace Polar
{
    public class HitDetection : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out ICollidable hit))
            {
                hit.OnCollision();
            }
        }
    }
}