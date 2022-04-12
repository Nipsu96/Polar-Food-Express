using UnityEngine;

namespace Polar
{
    public class Obstacle : MonoBehaviour, ICollidable
	{
		public ICollidable.ObjectType objectType;

		private void Start()
		{
			CheckType();
		}

		private void CheckType()
		{
			if (objectType == ICollidable.ObjectType.None)
			{
				Debug.LogError(gameObject.name + "'s type is none and it can't be!");
			}
		}

		public void OnCollision()
        {
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
			return objectType;
		}
	}
}