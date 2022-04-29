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
		internal bool enableControls = true;
		private bool tap;
		private Vector2 touchPosition;
		private Vector3 touchPosOffsetted;
		private float rayZOffset = -5.0f;
		private Vector2 direction;
		private Rigidbody2D playerRigidbody;
		public float gravity = 1;
		[SerializeField]
		private float jumpforce = 30f;
		public float linearDrag = 20f;

		public float gravityInAir = 1.0f;
		public float fallMultiplier = 9f;

		//[SerializeField]
		//private float minUpX= -1.0f;

		[SerializeField]
		private float maxUpX = 1.0f;

		private GroundCheck groundCheck;
		private bool isJumping;

		private Animator animator;

		public void Awake()
		{
			playerRigidbody = GetComponent<Rigidbody2D>();
			groundCheck = GetComponentInChildren<GroundCheck>();
		}

		private void Start()
		{
			animator = GetComponent<Animator>();
		}

		private void FixedUpdate()
		{
			ModifyPhysics();
		}

		// private void MoveCharacter()
		// {
		// 			Vector2 movement = moveInput * Time.deltaTime * velocity;
		// 			// transform property allows us to read and manipulate GameObject's position
		// 			// in the game world.
		// 			transform.Translate(movement);
		// }
		void ModifyPhysics()
		{
			bool changingDirections = (direction.x > 0 && playerRigidbody.velocity.x < 0) || (direction.x < 0 && playerRigidbody.velocity.x > 0);

			if (groundCheck.isGrounded)
			{
				//if (Mathf.Abs(direction.x) < 0.4f || changingDirections) {
				//    playerRigidbody.drag = linearDrag;
				//} else {
				//    playerRigidbody.drag = 0f;
				//}
				playerRigidbody.gravityScale = 0;
				//anim.SetTrigger("landed");
				animator.SetBool("isFalling", false);
				animator.SetBool("isFloating", false);
				animator.SetBool("isJumping", false);
				animator.SetBool("isLanding", true);
				//print("landed");
			}
			else
			{
				playerRigidbody.gravityScale = gravity;
				playerRigidbody.drag = linearDrag * 0.15f;

				if (playerRigidbody.velocity.y < 0)
				{
					playerRigidbody.gravityScale = gravity * fallMultiplier;
					//anim.SetTrigger("falling");
					animator.SetBool("isFloating", false);
					animator.SetBool("isLanding", false);
					animator.SetBool("isJumping", false);
					animator.SetBool("isFalling", true);
					//print("falling");
				}
				else if (playerRigidbody.velocity.y > 0 && playerRigidbody.velocity.y < maxUpX)
				{
					playerRigidbody.gravityScale = gravity * gravityInAir;
					//anim.SetTrigger("float");
					animator.SetBool("isJumping", false);
					animator.SetBool("isFalling", false);
					animator.SetBool("isLanding", false);
					animator.SetBool("isFloating", true);
					//print("float");
				}
				else if (playerRigidbody.velocity.y > 0)
				{
					playerRigidbody.gravityScale = gravity * (fallMultiplier / 2);
					//anim.SetTrigger("jumped");
					animator.SetBool("isLanding", false);
					animator.SetBool("isFalling", false);
					animator.SetBool("isFloating", false);
					animator.SetBool("isJumping", true);
					//print("jumped");
				}
			}
		}

		private void OnTouchPosition(InputAction.CallbackContext ctx)
		{
			//print("touch position");
			//touchPosition = ctx.ReadValue<Vector2>();
		}

		private void Update()
		{
			Debug.DrawRay(touchPosOffsetted, transform.TransformDirection(Vector3.forward) * 10.0f, Color.red);
		}

		private void OnJump(InputAction.CallbackContext callbackContext)
		{
			// InputActionPhase inputPhase = callbackContext.phase;
			// isJumping = inputPhase == InputActionPhase.Performed;

			// Old jump's read value
			//tap = callbackContext.ReadValueAsButton();

			tap = callbackContext.performed;
			print(callbackContext.phase);

			//touchPosition = callbackContext.ReadValue<Vector2>();
			//if (callbackContext.performed == true)
			//{
			//	print("tap: " + callbackContext.ReadValueAsButton());
			//}

			// RayCast
			touchPosOffsetted = new Vector3(touchPosition.x, touchPosition.y, rayZOffset);

			int layerMask = 5;

			RaycastHit2D hit = Physics2D.Raycast(touchPosOffsetted, Vector3.forward, 10.0f);
			if (hit.collider == null)
			{
				PlayerJump();
				//print("RayCast hit: " + hit);
			}
			else
			{
				//print("hit null");
			}
		}
		private void PlayerJump()
		{
			if (tap && groundCheck.isGrounded && enableControls)
			{
				// hyppäämisen koodi tänne
				playerRigidbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
			}

		}

		public void EnableControls()
		{
			enableControls = true;
		}

		public void DisableControls()
		{
			enableControls = false;
		}
	}
}