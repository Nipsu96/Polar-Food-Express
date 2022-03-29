using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private float timer = 5.0f;
        private float spawnTimer;
        [SerializeField] private Transform spawnPointA;
        [SerializeField] private Transform spawnPointB;
		[SerializeField] private ObjectPooler goodFoodPool;
		[SerializeField] private ObjectPooler badFoodPool;
		[SerializeField] private ObjectPooler obstaclePool;
		[SerializeField] private List<ObjectPooler> objectPools;
		private int index;

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
				index = Random.Range(0, 3);
				Debug.Log("Random 1: " + index);
				SpawnObject(spawnPointA, objectPools[index]);
				index = Random.Range(0, 2);
				Debug.Log("Random 2: " + index);
				SpawnObject(spawnPointB, objectPools[index]);
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
            if(incomingObject != null)
            {
                incomingObject.transform.position = spawnPoint.transform.position;
                incomingObject.transform.rotation = spawnPoint.transform.rotation;
                incomingObject.SetActive(true);
            }
        }
    }
}
