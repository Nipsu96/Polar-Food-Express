using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class AutomoveObject : MonoBehaviour
    {
		[SerializeField, Tooltip("Lower than 0 = object moves slower than than basic objects, higher than 0 = object moves faster than basic objects.")] private float parallaxSpeedOffset;
		[SerializeField] private bool isBackgroundElement;

		private float width;

		private void OnEnable()
		{
			GameManager.GameOver += DisableThisScript;
		}

		private void OnDisable()
		{
			GameManager.GameOver -= DisableThisScript;
		}

		private void DisableThisScript()
		{
			Destroy(this);
		}

		private void FixedUpdate()
        {
			if(isBackgroundElement)
			{
				if(transform.position.x < -width)
				{
					Reposition();
				}
			}
			else
			{
				float moveSpeed = GameManager.Instance.gameSpeed + parallaxSpeedOffset;
	            transform.Translate(Vector3.left * moveSpeed * Time.fixedDeltaTime);
			}
        }

		private void Reposition()
		{
			Vector2 vector = new Vector2(width * 2f, 0);
			transform.position = (Vector2)transform.position + vector;
		}
    }
}
