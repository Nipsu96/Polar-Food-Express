using UnityEngine;

namespace Polar
{
    public class Obstacle : MonoBehaviour, ICollidable
    {
        public void OnCollision()
        {
            //Debug.LogWarning(this.name + " collided with a bear.");

            // Calls GameManager to end the game
            GameManager.Instance.EndGame();
        }
    }
}