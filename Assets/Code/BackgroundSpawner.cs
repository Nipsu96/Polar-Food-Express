using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
	[RequireComponent(typeof(ObjectPooler))]
    public class BackgroundSpawner : MonoBehaviour
	{
		[SerializeField] private float timer = 5.0f;
		private float spawnTimer;
		[SerializeField] private Transform spawnPoint;
		private ObjectPooler objectPool;

		private void Start()
		{
			GetPools();
			ResetCountdown();
		}

		private void FixedUpdate()
		{
			Countdown();
		}

		private void GetPools()
		{
			objectPool = GetComponent<ObjectPooler>();
		}

		private void Countdown()
		{
			spawnTimer -= Time.deltaTime;
			if (spawnTimer <= 0.0f)
			{
				//RandomizeSpawnPool();
				SpawnObject();
				ResetCountdown();
			}
		}

		private void SpawnObject()
		{
			print("Spawn");
			GameObject incomingObject = objectPool.GetPooledObjects();
			if (incomingObject != null)
			{
				incomingObject.transform.position = spawnPoint.transform.position;
				incomingObject.transform.rotation = spawnPoint.transform.rotation;
				incomingObject.SetActive(true);
			}
		}

		private void ResetCountdown()
		{
			spawnTimer = timer;
		}
	}
}
