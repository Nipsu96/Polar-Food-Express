using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class AutomoveObject : MonoBehaviour
    {
        private void FixedUpdate()
        {
            float moveSpeed = GameManager.Instance.gameSpeed;
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}
