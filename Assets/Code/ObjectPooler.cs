using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Polar
{
    public class ObjectPooler : MonoBehaviour
    {
        internal static ObjectPooler Instance;
        [SerializeField] private List<GameObject> pooledObjects;
        [SerializeField] private GameObject prefabToPool;
        [SerializeField] private int amountToPool;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            pooledObjects = new List<GameObject>();
            GameObject temp;
            for(int i = 0; i < amountToPool; i++)
            {
                temp = Instantiate(prefabToPool);
                temp.SetActive(false);
                pooledObjects.Add(temp);
            }
        }

        internal GameObject GetPooledObjects()
        {
            for (int i = 0; i < amountToPool; i++)
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
