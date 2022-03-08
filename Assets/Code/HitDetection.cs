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

        // Old, not is use
        //private void OnTriggerEnter2D(Collider2D player)
        //{
        //    // Check does object enter in the the player's trigger area
        //    if (player.GetComponent<PlayerController>() != null)
        //    {
        //        Collectable collectable = GetComponent<Collectable>();
        //        // Set score
        //        //ScoreManager.Instance.currentScore += collectable.score;
        //        ScoreManager.Instance.AddScore(1);

        //        // Set Carbon Footprint
        //        CarbonManager.Instance.AddCarbon(1);

        //        // TODO: Change Destroy() to pooling
        //        Destroy(gameObject);
        //    }
        //}
    }
}
