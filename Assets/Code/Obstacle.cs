using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
			// // Calls GameManager to end the game
			//  float delay = 0;

			// Disable visuals if scene is not tutorial
			Scene currentScene = SceneManager.GetActiveScene();
			if (currentScene.name != "Tutorial")
			{
				SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
				spriteRenderer.enabled = false;
			}

            AudioSource hitAudio = gameObject.GetComponent<AudioSource>();
            if (hitAudio != null)
            {
                hitAudio.Play();
                // delay = hitAudio.clip.length;
            }
            // StartCoroutine(Delay(delay));
            GameManager.Instance.EndGame();


        }

        // IEnumerator Delay(float delay)
        // {
        //     yield return new WaitForSeconds(delay);
        //     GameManager.Instance.EndGame();
        // }
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