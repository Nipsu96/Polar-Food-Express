using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Polar
{
    public class AutomoveObject : MonoBehaviour
    {
		[SerializeField, Tooltip("Lower than 0 = object moves slower than than basic objects, higher than 0 = object moves faster than basic objects.")] private float parallaxSpeedOffset;
		[SerializeField] private bool ceilingParalaxFixForTutorial = false;

		private void OnEnable()
		{
			GameManager.GameOver += DisableThisScript;

			if (ceilingParalaxFixForTutorial)
			{
				Scene currentScene = SceneManager.GetActiveScene();
				if (currentScene.name == "Tutorial")
				{
					TutorialManager.StartGameplayLoop += SetParallaxSpeed;
				}
			}
		}

		private void OnDisable()
		{
			GameManager.GameOver -= DisableThisScript;

			if (ceilingParalaxFixForTutorial)
			{
				Scene currentScene = SceneManager.GetActiveScene();
				if (currentScene.name == "Tutorial")
				{
					TutorialManager.StartGameplayLoop -= SetParallaxSpeed;
				} 
			}
		}

		private void DisableThisScript()
		{
			Destroy(this);
		}

		private void SetParallaxSpeed()
		{
			parallaxSpeedOffset = -8.0f;
		}

		private void FixedUpdate()
        {
            float moveSpeed = GameManager.Instance.gameSpeed + parallaxSpeedOffset;
            transform.Translate(Vector3.left * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
