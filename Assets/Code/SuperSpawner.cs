using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
	[RequireComponent(typeof(ObjectPooler))]
    public class SuperSpawner : MonoBehaviour
	{
		[SerializeField] private float timer = 5.0f;
		private float spawnTimer;
		[SerializeField] private Transform spawnPointA;
		[SerializeField] private Transform spawnPointB;
		[SerializeField] private ObjectPooler objectPool;
		//[SerializeField] private List<ObjectPooler> objectPools;
		private int index;

		private void Start()
		{
			objectPool = GetComponent<ObjectPooler>();
			ResetCountdown();

			print("Pooler: " + objectPool.gameObject.GetComponent<Collectable>().foodType);
		}

		private void FixedUpdate()
		{
			Countdown();
		}

		private void Countdown()
		{
			spawnTimer -= Time.deltaTime;
			if (spawnTimer <= 0.0f)
			{
				SpawnObject(spawnPointA, objectPool);
				SpawnObject(spawnPointB, objectPool);
				ResetCountdown();
			}
		}

		private void ResetCountdown()
		{
			spawnTimer = timer;
		}

		private void SpawnObject(Transform spawnPoint, ObjectPooler pool)
		{
			GameObject incomingObject = pool.GetPooledObjects();
			if (incomingObject != null)
			{
				incomingObject.transform.position = spawnPoint.transform.position;
				incomingObject.transform.rotation = spawnPoint.transform.rotation;
				incomingObject.SetActive(true);
			}
		}
	}
}