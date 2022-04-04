using UnityEngine;

namespace Polar
{
    public class Obstacle : MonoBehaviour, ICollidable
	{
		//public enum ObstacleType
		//{
		//	None,
		//	GroundObstacle,
		//	AerialObstacle
		//}
		//[SerializeField] public ObstacleType obstacleType;

		//enum ObjectType;

		[SerializeField] private ICollidable.ObjectType objectType;

		private void Start()
		{
			//if (obstacleType == ObstacleType.None)
			//{
			//	Debug.LogError(gameObject.name + "'s type is none and it can't be!");
			//}
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

		public ICollidable.ObjectType GetObjectType()
		{
			//print("GetType");
			return objectType;
		}
	}
}