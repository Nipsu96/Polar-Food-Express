using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class BackgroundLoop : MonoBehaviour
    {
		[SerializeField] private GameObject[] backgrounds;
		private Camera cam;
		private Vector2 screenBounds;

		private void Start()
		{
			cam = Camera.main;
			screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));

			foreach (GameObject background in backgrounds)
			{
				LoadBackgrounds(background);
			}
		}

		private void LoadBackgrounds(GameObject bg)
		{
			float bgWidth = bg.GetComponent<SpriteRenderer>().bounds.size.x;
			int childrenNeeded = (int)Mathf.Ceil(screenBounds.x * 3 / bgWidth);
			// TODO: take random from pool
			GameObject clone = Instantiate(bg);
			for (int i = 0; i < childrenNeeded; i++)
			{
				// TODO: take random from pool
				GameObject c = Instantiate(clone);
				c.transform.SetParent(bg.transform);
				c.transform.position = new Vector3(bgWidth * i, bg.transform.position.y, bg.transform.position.z);
				c.name = bg.name + i;
			}
			Destroy(clone);
			Destroy(bg.GetComponent<SpriteRenderer>());
		}

		private void RepositionChildren(GameObject bg)
		{
			Transform[] children = bg.GetComponentsInChildren<Transform>();
			if(children.Length > 1)
			{
				GameObject firstChild = children[1].gameObject;
				GameObject lastChild = children[children.Length - 1].gameObject;
				float halfBackgroundWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
				print("pos " + halfBackgroundWidth);
				if(transform.position.x + screenBounds.x > lastChild.transform.position.x + halfBackgroundWidth)
				{
					firstChild.transform.SetAsLastSibling();
					firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfBackgroundWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
				}
				else if(transform.position.x - screenBounds.x < firstChild.transform.position.x - halfBackgroundWidth)
				{
					lastChild.transform.SetAsFirstSibling();
					lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfBackgroundWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
				}
			}
		}

		private void LateUpdate()
		{
			foreach (GameObject background in backgrounds)
			{
				RepositionChildren(background);
			}
		}
	}
}
