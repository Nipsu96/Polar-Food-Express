using UnityEngine;

namespace Polar
{
    public class Obstacle : MonoBehaviour, ICollidable
	{
		enum ObstacleType
		{
			None,
			GroundObstacle,
			AerialObstacle
		}
		[SerializeField] private ObstacleType obstacleType;

		private void Start()
		{
			if (obstacleType == ObstacleType.None)
			{
				Debug.LogError(gameObject.name + "'s type is none and it can't be!");
			}
		}
		public void OnCollision()
        {
            //Debug.LogWarning(this.name + " collided with a bear.");

            // Calls GameManager to end the game
            GameManager.Instance.EndGame();
        }

		public void OnDespawn()
		{
			// Set this obstacle inactive if it collides with a despawner.
			gameObject.SetActive(false);
		}
	}
}