using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Polar
{
	[RequireComponent(typeof(InputProcessor))]
	public class PlayerController : MonoBehaviour
	{
        private bool tap ;
		private Vector2 direction;
        private Rigidbody2D playerRigidbody;
		public float gravity = 1;
        [SerializeField]    
        private float jumpforce = 30f;
		public float linearDrag = 20f;
		// public float linearDrag = 20f;
		public float fallMultiplier = 9f;

        private GroundCheck groundCheck;

		// private Vector2 touchPosition;
        public void Awake(){
			playerRigidbody = GetComponent<Rigidbody2D>();
            groundCheck= GetComponentInChildren<GroundCheck>();

		}

    
		private void FixedUpdate()
		{
			modifyPhysics();
           
		}

		// private void MoveCharacter()
		// {
		// 			Vector2 movement = moveInput * Time.deltaTime * velocity;
		// 			// transform property allows us to read and manipulate GameObject's position
		// 			// in the game world.
		// 			transform.Translate(movement);
		// }
		void modifyPhysics() {
        bool changingDirections = (direction.x > 0 && playerRigidbody.velocity.x < 0) || (direction.x < 0 && playerRigidbody.velocity.x > 0);

        if(groundCheck.isGrounded){
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections) {
                playerRigidbody.drag = linearDrag;
            } else {
                playerRigidbody.drag = 0f;
            }
            playerRigidbody.gravityScale = 0;
        }else{
            playerRigidbody.gravityScale = gravity;
            playerRigidbody.drag = linearDrag * 0.15f;
            if(playerRigidbody.velocity.y < 0){
                playerRigidbody.gravityScale = gravity * fallMultiplier;
            }else if(playerRigidbody.velocity.y > 0 && !tap){
                playerRigidbody.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

		private void OnJump(InputAction.CallbackContext callbackContext)
		{
            tap = callbackContext.ReadValueAsButton();
            PlayerJump();
		}
        private void PlayerJump()
		{
            if(tap && groundCheck.isGrounded){
                // hyppäämisen koodi tänne
                // playerRigidbody.AddForce(new Vector2(0.0f,jumpforce),ForceMode2D.Impulse);
				playerRigidbody.AddForce(Vector2.up*jumpforce,ForceMode2D.Impulse);
            }
            
		}

	}
}