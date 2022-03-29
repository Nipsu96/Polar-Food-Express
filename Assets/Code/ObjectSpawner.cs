using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPointA;
        [SerializeField] private Transform spawnPointB;
        [SerializeField] private float timer = 5.0f;
        private float spawnTimer;

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
                SpawnObjects();
                ResetCountdown();
            }
        }

        private void ResetCountdown()
        {
            spawnTimer = timer;
        }

        private void SpawnObjects()
        {
            GameObject incomingObject = ObjectPooler.Instance.GetPooledObjects();
            if(incomingObject != null)
            {
                incomingObject.transform.position = spawnPointA.transform.position;
                incomingObject.transform.rotation = spawnPointA.transform.rotation;
                incomingObject.SetActive(true);
            }
        }
    }
}
