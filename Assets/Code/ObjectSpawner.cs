using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Polar
{
    public class ObjectSpawner : MonoBehaviour
    {
		[SerializeField] private bool enableMainMenuSettings = false;
        [SerializeField] private float timer = 5.0f;
		[SerializeField, ShowIf("enableMainMenuSettings")] private float maxTimer = 2.0f;
		private float spawnTimer;
        [SerializeField, HideIf("enableMainMenuSettings")] private Transform airSpawnPoint;
        [SerializeField] private Transform groundSpawnPoint;
		[SerializeField] private List<ObjectPooler> objectPools;
		private int objectPoolAIndex;
		private int objectPoolBIndex;
		[SerializeField, HideIf("enableMainMenuSettings")] private bool alwaysSpawnGoodFood = true;

		private void Start()
        {
            ResetCountdown();
        }

		private void OnValidate()
		{
			maxTimer = Mathf.Max(timer, maxTimer);
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
				if(!enableMainMenuSettings)
				{
					// In game
					RandomizeSpawnPool();
				}
				else
				{
					// Main menu
					SpawnObject(groundSpawnPoint, objectPools[0]);
				}
				ResetCountdown();
			}
		}

		private void RandomizeSpawnPool()
		{
			// Set temp collidable
			ICollidable tmpCollidable = objectPools[0].objectsToPool[0].GetComponent<ICollidable>();

			// Randomize which pool to use(Good, Bad, GroundObstacle) for ground spawn point.
			do
			{
				objectPoolAIndex = Random.Range(0, objectPools.Count);
				tmpCollidable = objectPools[objectPoolAIndex].objectsToPool[0].GetComponent<ICollidable>();
			}
			// The ground object can't be an AerialObstacle.
			while (tmpCollidable.GetObjectType() == ICollidable.ObjectType.AerialObstacle);

			// Spawn the object to ground spawn point.
			SpawnObject(groundSpawnPoint, objectPools[objectPoolAIndex]);

			// Set the second spawned object to good food if the first one is not.
			if (tmpCollidable.GetObjectType() != ICollidable.ObjectType.GoodFood && alwaysSpawnGoodFood)
			{
				objectPoolBIndex = (int)ICollidable.ObjectType.GoodFood - 1;
			}
			else
			{
				tmpCollidable = RandomizeAerialSpawn();
			}

			// Spawn the object to the aerial spawn point.
			SpawnObject(airSpawnPoint, objectPools[objectPoolBIndex]);
		}

		private ICollidable RandomizeAerialSpawn()
		{
			ICollidable tmpCollidable;
			do
			{
				// Randomize which pool to use (Good, Bad, AerialObstacle) for aerial spawn point.
				objectPoolBIndex = Random.Range(0, objectPools.Count);
				tmpCollidable = objectPools[objectPoolBIndex].objectsToPool[0].GetComponent<ICollidable>();
			}
			// Rerandomize if
			while ( // To be spawned objects are the same.
					objectPoolBIndex == objectPoolAIndex ||
					// The aerial object is trying to be a GroundObstacle.
					tmpCollidable.GetObjectType() == ICollidable.ObjectType.GroundObstacle ||
					// Both the ground and the aerial objects are trying to be obstacles.
					(objectPools[objectPoolAIndex].objectsToPool[0].GetComponent<Obstacle>() && objectPools[objectPoolBIndex].objectsToPool[0].GetComponent<Obstacle>()));
			return tmpCollidable;
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
			if (!enableMainMenuSettings)
			{
				// In game
	            spawnTimer = timer;
			}
			else
			{
				// Main menu
				spawnTimer = Random.Range(timer, maxTimer);
			}
        }
    }
}