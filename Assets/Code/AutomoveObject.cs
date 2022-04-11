using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class AutoMoveObject : MonoBehaviour
    {
		[SerializeField, Tooltip("Lower than 0 = object moves slower than than basic objects, higher than 0 = object moves faster than basic objects.")] private float parallaxSpeedOffset;

        private void FixedUpdate()
        {
            float moveSpeed = GameManager.Instance.gameSpeed + parallaxSpeedOffset;
            transform.Translate(Vector3.left * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
