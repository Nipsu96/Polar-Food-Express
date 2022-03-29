using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class Collectable : MonoBehaviour, ICollidable
    {
        [SerializeField] private SOCollectableValues values;
		internal enum FoodType
		{
			None,
			GoodFood,
			BadFood
		}
		[SerializeField] internal FoodType foodType;

		private void Start()
		{
			if(foodType == FoodType.None)
			{
				Debug.LogError(gameObject.name + "'s food quality is none and it can't be!");
			}
		}

		public void OnCollision()
        {
            SetScore();
            SetCarbon();
			OnDespawn();
		}

		public void OnDespawn()
		{
			// Set this collectable inactive if it collides with the bear or a despawner.
			gameObject.SetActive(false);
		}

		private void SetScore()
        {
            ScoreManager.Instance.AddScore(values.score);
        }

        private void SetCarbon()
        {
            CarbonManager.Instance.AddCarbon(values.carbonImpact);
        }
	}
}