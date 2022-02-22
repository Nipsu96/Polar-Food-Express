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
		private Vector2 moveInput;
        private Rigidbody2D playerRigidbody;
        [SerializeField]    
        private float jumpforce = 3.0f;

        private GroundCheck groundCheck;

		// private Vector2 touchPosition;
        public void Awake(){
			playerRigidbody = GetComponent<Rigidbody2D>();
            groundCheck= GetComponentInChildren<GroundCheck>();

		}

    
		private void Update()
		{
			// MoveCharacter();
           
		}

		// private void MoveCharacter()
		// {
		// 			Vector2 movement = moveInput * Time.deltaTime * velocity;
		// 			// transform property allows us to read and manipulate GameObject's position
		// 			// in the game world.
		// 			transform.Translate(movement);
		// }

		private void OnJump(InputAction.CallbackContext callbackContext)
		{
            tap = callbackContext.ReadValueAsButton();
            PlayerJump();
		}
        private void PlayerJump()
		{
            if(tap && groundCheck.isGrounded){
                // hyppäämisen koodi tänne
                playerRigidbody.AddForce(new Vector2(0.0f,jumpforce),ForceMode2D.Impulse);
            }
            
		}

	}
}