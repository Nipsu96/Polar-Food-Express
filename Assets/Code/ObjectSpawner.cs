using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private float timer = 5.0f;
        private float spawnTimer;
        [SerializeField] private Transform airSpawnPoint;
        [SerializeField] private Transform groundSpawnPoint;
		//[SerializeField] private ObjectPooler goodFoodPool;
		//[SerializeField] private ObjectPooler obstaclePool;
		//[SerializeField] private ObjectPooler badFoodPool;
		[SerializeField] private List<ObjectPooler> objectPools;
		private int indexA;
		private int indexB;

		private void Start()
        {
            ResetCountdown();
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
				// Randomize which pool to use (Good, Bad, Obstacle)
				indexA = Random.Range(0, 3);
				//print("IndexA: " + indexA);
				// Spawn object to spawn point A (on the ground)
				SpawnObject(groundSpawnPoint, objectPools[indexA]);


				// Randomize which pool to use (Good, Bad, Obstacle)
				// Rerandomize if B pool is the same than the A pool
				while (indexB == indexA)
				{
					indexB = Random.Range(0, 3);
					//print("IndexB: " + indexB);
				}
				// Spawn object to spawn point B (in the air)
				SpawnObject(airSpawnPoint, objectPools[indexB]);

				ResetCountdown();
            }
		}

		private void SpawnObject(Transform spawnPoint, ObjectPooler pool)
		{
			// TODO: Check which object to use (aerial vs. gound)
			GameObject incomingObject = pool.GetPooledObjects(spawnPoint);
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