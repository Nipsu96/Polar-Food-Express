using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class DisableObjectOnEvent : MonoBehaviour
    {
		[SerializeField] private float delay = 0.1f;
		private bool gameOver;

		private void OnEnable()
		{
			// Listen the event, fire up method if event is being started.
			GameManager.GameOver += EnableGameOver;
		}

		private void OnDisable()
		{
			// Stop listening the event.
			GameManager.GameOver -= EnableGameOver;
		}

		private void EnableGameOver()
		{
			gameOver = true;
		}

		private void DisableObject()
		{
			gameOver = false;
			gameObject.SetActive(false);
		}

		private void FixedUpdate()
		{
			if(gameOver)
			{
				delay -= Time.deltaTime;
			}

			if (delay <= 0.0f)
			{
				DisableObject();
			}
		}
	}
}
