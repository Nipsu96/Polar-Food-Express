using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class HitDetection : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D player)
        {
            // Check does object enter in the the player's trigger area
            if (player.GetComponent<PlayerController>() != null)
            {
                Collectable collectable = GetComponent<Collectable>();
                // Set score
                ScoreManager.Instance.currentScore += collectable.score;

                // Set Carbon Footprint
                CarbonManager.Instance.currentCarbonFootprint += collectable.carbonFootprintImpact;

                // TODO: Change Destroy() to pooling
                Destroy(gameObject);
            }
        }
    }
}
