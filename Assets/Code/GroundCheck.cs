using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class GroundCheck : MonoBehaviour
    {
        internal bool isGrounded;
        
        [SerializeField]
        private LayerMask ground;
        private void OnTriggerEnter2D(Collider2D other) {
           isGrounded = other != null && (((1 << other.gameObject.layer ) & ground ) !=0);

        }
        private void OnTriggerExit2D(Collider2D other) {
            isGrounded = false;
        }
    }
}