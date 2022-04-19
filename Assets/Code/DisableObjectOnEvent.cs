using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class DisableObjectOnEvent : MonoBehaviour
    {
		private void OnEnable()
		{
			// Listen the event, fire up method if event is being started.
			GameManager.GameOver += DisableObject;
		}

		private void OnDisable()
		{
			// Stop listening the event.
			GameManager.GameOver -= DisableObject;
		}

		private void DisableObject()
		{
			gameObject.SetActive(false);
		}
    }
}
