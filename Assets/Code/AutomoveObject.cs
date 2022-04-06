using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class AutoMoveObject : MonoBehaviour
    {
		[SerializeField] private float speedOffset;

        private void FixedUpdate()
        {
            float moveSpeed = GameManager.Instance.gameSpeed + speedOffset;
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}
