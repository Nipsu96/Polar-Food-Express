using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
	public class BGLooper : MonoBehaviour
	{
		[SerializeField] private ObjectPooler backgrounds;
		//[SerializeField] private ObjectPooler floors;
		//[SerializeField] private ObjectPooler ceilings;

		[SerializeField] private GameObject firstBackground;
		[SerializeField] private GameObject secondBackground;

		[SerializeField] private Transform spawnPoint;

		// new system
		private float xOffset;

		//	for (int i = 0; i< 5; i++)
		//		{
		//		Sprite sprite = BuildingsSprites[Random.Range(0, 5)];
		//	float currentWidth = sprite.bounds.size.x;
		//	GameObject Background = (GameObject)Instantiate(BuildingObjectPrefab, nextPos, Quaternion.identity);
		//	Background.GetComponent<SpriteRenderer>().sprite = sprite;
		//        nextPos += new Vector3(currentWidth / 2, 0, 0);
		//	Sprite nextSprite = BuildingsSprites[Random.Range(0, 5)];
		//	float nextWidth = nextSprite.bounds.size.x;
		//	GameObject Background2 = (GameObject)Instantiate(BuildingObjectPrefab, nextPos, Quaternion.identity);
		//	Background2.GetComponent<SpriteRenderer>().sprite = nextSprite;
		//        nextPos += new Vector3(nextWidth / 2, 0, 0);
		//}

		private void Start()
		{
			//print("firstBackground: " + firstBackground.GetComponent<SpriteRenderer>().sprite.bounds);
			//print("firstBackground: " + firstBackground.GetComponent<SpriteRenderer>().sprite.bounds.extents);
			//print("secondBackground: " + secondBackground.GetComponent<SpriteRenderer>().sprite.bounds);
			//print("secondBackground: " + secondBackground.GetComponent<SpriteRenderer>().sprite.bounds.extents);

			//Instantiate(firstBackground, new Vector3(0.0f, 0, 0), Quaternion.identity);
			
			xOffset = firstBackground.GetComponent<SpriteRenderer>().sprite.bounds.extents.x + secondBackground.GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
			Vector3 secondSpawn = new Vector3(xOffset, 0.0f);

			//Instantiate(secondBackground, secondSpawn, Quaternion.identity);

			SpawnObject(backgrounds);
		}

		private void SpawnObject(ObjectPooler pool)
		{
			GameObject background = pool.GetPooledObjects();
			if (background != null)
			{
				background.transform.position = spawnPoint.transform.position;
				background.transform.rotation = spawnPoint.transform.rotation;
				background.SetActive(true);
			}
		}

		//float prevWidth = 1;
		//Vector3 nextPos = BuildingsPos;

		//   for (int i = 0; i < 25; i++)
		//{
		//       Sprite sprite = BuildingsSprites[Random.Range(0, 5)];
		//	float currentWidth = sprite.bounds.size.x;
		//	nextPos += new Vector3(currentWidth / 2 + prevWidth / 2, 0, 0);
		//	GameObject Background = (GameObject)Instantiate(SpawnedObjectHolder, nextPos, Quaternion.identity);
		//	Background.GetComponent<SpriteRenderer>().sprite = sprite;
		//       prevWidth = currentWidth;
	}
}