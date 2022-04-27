using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class Collectable : MonoBehaviour, ICollidable
    {
        [SerializeField] private SOCollectableValues values;
        public ICollidable.ObjectType objectType;
		private Collider2D col;
		private SpriteRenderer background;
		private SpriteRenderer food;

        private void Start()
		{
			CheckType();
			GetComponents();
		}

		private void GetComponents()
		{
			col = GetComponent<Collider2D>();
			background = GetComponent<SpriteRenderer>();
			food = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
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
			SetScore();
			SetCarbon();
			float delay = 0;
			AudioSource hitAudio = gameObject.GetComponent<AudioSource>();
			if (hitAudio != null)
			{
				hitAudio.Play();
				delay = hitAudio.clip.length;
			}

			SetVisualsAndCollider(false);

			StartCoroutine(Delay(delay));

		}

		private void SetVisualsAndCollider(bool turnVisibility)
		{
			col.enabled = turnVisibility;
			background.enabled = turnVisibility;
			food.enabled = turnVisibility;
		}

		IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
            OnDespawn();
        }
        public void OnDespawn()
        {
            // Set this collectable inactive if it collides with the bear or a despawner.
            gameObject.SetActive(false);

			// Set visual and collider back on, otherwise object will be broken when respawned.
			SetVisualsAndCollider(true);
		}

        private void SetScore()
        {
            ScoreManager.Instance.AddScore(values.score);
        }

        private void SetCarbon()
        {
            CarbonManager.Instance.AddCarbon(values.carbonImpact);
        }

        ICollidable.ObjectType ICollidable.GetObjectType()
        {
            return objectType;
        }
    }
}