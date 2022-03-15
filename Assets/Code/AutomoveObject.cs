using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Polar
{
    public class AutoMoveObject : MonoBehaviour
    {
        private void FixedUpdate()
        {
            float moveSpeed = GameManager.Instance.gameSpeed;
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}
