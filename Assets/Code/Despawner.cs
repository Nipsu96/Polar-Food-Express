using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class Despawner : MonoBehaviour
    {
		private void OnTriggerEnter2D(Collider2D other)
		{
			if(other.TryGetComponent(out ICollidable hit))
			{
				print(other.gameObject.name + " collided with " + name);
				hit.OnDespawn();
			}
		}
	}
}