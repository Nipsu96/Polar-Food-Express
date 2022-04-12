using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class ObjectPooler : MonoBehaviour
    {
        internal List<GameObject> pooledObjects;
        [SerializeField] internal List<GameObject> objectsToPool;
		[SerializeField] private int amountToPool = 5;

        private void Start()
		{
			PoolObjects();
		}

		private void PoolObjects()
		{
			pooledObjects = new List<GameObject>();

			for (int j = 0; j < objectsToPool.Count; j++)
			{
				// Create new folder for objects to be pooled
				GameObject pool = new GameObject($"PoolOf{objectsToPool[j].name}");

				for (int i = 0; i < amountToPool; i++)
				{
					GameObject temp = Instantiate(objectsToPool[j]);

					// Add object to it's own pool
					temp.transform.SetParent(pool.transform);

					temp.SetActive(false);
					pooledObjects.Add(temp);
				} 
			}
		}

		internal GameObject GetPooledObjects(Transform spawnPoint)
        {
			for (int i = 0; i < pooledObjects.Count; i++)
			{
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
			}
            return null;
        }
    }
}