using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class Obstacle : MonoBehaviour, ICollidable
    {
        public void OnCollision()
        {
            Debug.LogWarning(this.name + " collided with a bear.");
            // GameManager gameSpeed
            // Obstacle call SetGameSpeed 0 OnCollision
        }
    }
}
