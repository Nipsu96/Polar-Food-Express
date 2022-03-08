using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class AutoMoveObject : MonoBehaviour
    {
        [SerializeField] private SOGameSpeed gameSpeed;
        //[SerializeField, Range(0.1f, 100.0f)] private float moveSpeed = 1.0f;

        private void FixedUpdate()
        {
            transform.Translate(Vector3.left * gameSpeed.gameSpeed * Time.deltaTime);
        }
    }
}
