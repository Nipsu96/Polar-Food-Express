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
				RandomizeSpawnPool();
				ResetCountdown();
			}
		}

		private void RandomizeSpawnPool()
		{
			// Randomize which pool to use (Good, Bad, GroundObstacle).
			indexA = Random.Range(0, 3);

			// Spawn the object to ground spawn point.
			SpawnObject(groundSpawnPoint, objectPools[indexA]);

			// Set collidable
			ICollidable collidable = objectPools[0].objectsToPool[0].GetComponent<ICollidable>();

			// Randomize which pool to use (Good, Bad, AerialObstacle)
			do
			{
				indexB = Random.Range(0, 4);
				collidable = objectPools[indexB].objectsToPool[0].GetComponent<ICollidable>();
			}
			while ( // Spawnable objects can't be the same.
					indexB == indexA ||
					// The aerial object can't be a GroundObstacle.
					collidable.GetObjectType() == ICollidable.ObjectType.GroundObstacle ||
					// Both the ground and the aerial objects can't be obstacles.
					(objectPools[indexA].objectsToPool[0].GetComponent<Obstacle>() && objectPools[indexB].objectsToPool[0].GetComponent<Obstacle>()));

			// Spawn the object to the aerial spawn point.
			SpawnObject(airSpawnPoint, objectPools[indexB]);
		}

		private void SpawnObject(Transform spawnPoint, ObjectPooler pool)
		{
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