using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

namespace Polar
{
    public class TutorialManager : MonoBehaviour
    {
		enum Phase { Beginning, First, Second, Third, Four, Five, Six, Seven, Eight };
		private Phase phase;
		[SerializeField] private ObjectSpawner objectSpawner;
		[SerializeField] private GameObject playerCharacter;
		[SerializeField] private float waitTimeAtStart = 2.0f;
		private Rigidbody2D rbPolarBear;
		[SerializeField] private Transform bearPosition;
		[SerializeField] private float multiplier = 20.0f;
		private Animator animator;
		[SerializeField] private GameObject thoughtsPanel;
		[SerializeField] private Button okButton;
		[SerializeField] private TMP_Text textField;
		[SerializeField] private GameObject carbonMeter;
		[SerializeField] private GameObject carbonMultiplierText;
		[SerializeField] private GameObject carbonMultiplierValue;

		[Header("Texts")]
		[SerializeField, ResizableTextArea] private string firstText;
		[SerializeField, ResizableTextArea] private string secondText;
		[SerializeField, ResizableTextArea] private string thirdText;
		[SerializeField, ResizableTextArea] private string fourthText;
		[SerializeField, ResizableTextArea] private string fifthText;
		[SerializeField, ResizableTextArea] private string sixthText;

		private void Start()
		{
			InitializeSettings();
			StartCoroutine(WaitAtStart());
		}

		void FixedUpdate()
		{
			MoveBearToPosition();
		}

		private void MoveBearToPosition()
		{
			if (phase == Phase.First)
			{
				//rbPolarBear.transform.position = Vector2.MoveTowards(rbPolarBear.transform.position, bearPosition.transform.position, speed * Time.deltaTime);
				var dif = bearPosition.transform.position - rbPolarBear.transform.position;
				if (dif.magnitude > 1)
				{
					rbPolarBear.AddForce(dif * multiplier * Time.deltaTime);
				}
				else
				{
					rbPolarBear.velocity = Vector2.zero;
					//moveBear = false;
					animator.SetBool("idle", true);
					//firstThought = true;
					phase = Phase.Second;
				}
			}

			if (phase == Phase.Second)
			{
				thoughtsPanel.SetActive(true);
				textField.text = firstText;
			}
		}

		private void InitializeSettings()
		{
			phase = Phase.Beginning;
			GameManager.Instance.fixedGameSpeed = true;
			GameManager.Instance.gameSpeed = 2;
			objectSpawner.enableSpawning = false;
			playerCharacter.GetComponent<PlayerController>().enableControls = false;
			rbPolarBear = playerCharacter.GetComponent<Rigidbody2D>();
			animator = playerCharacter.GetComponent<Animator>();
			animator.enabled = false;
			thoughtsPanel.SetActive(false);
			//carbonMeter.SetActive(false);
			//carbonMultiplierText.SetActive(false);
			//carbonMultiplierValue.SetActive(false);
		}

		IEnumerator WaitAtStart()
		{
			yield return new WaitForSeconds(waitTimeAtStart);
			//moveBear = true;
			phase = Phase.First;
			animator.enabled = true;
		}

		private void SecondThough()
		{
			phase = Phase.Third;
			textField.text = secondText;
			thoughtsPanel.SetActive(true);
		}

		private void EnableGameplay()
		{
			phase = Phase.Four;

			GameManager.Instance.gameSpeed = 8.5f;
			objectSpawner.enableSpawning = true;
			playerCharacter.GetComponent<PlayerController>().enableControls = true;
			animator.SetBool("stopIdling", true);

			StartCoroutine(GoodFood());
		}

		IEnumerator GoodFood()
		{
			yield return new WaitForSeconds(2.5f);
			GoodFoodInfo();
		}

		private void GoodFoodInfo()
		{
			phase = Phase.Five;
			textField.text = thirdText;
			Time.timeScale = 0.0f;
			thoughtsPanel.SetActive(true);
		}

		IEnumerator ObstacleAndBadFoodInfo()
		{
			yield return new WaitForSeconds(2.0f);
			phase = Phase.Six;
			textField.text = fourthText;
			Time.timeScale = 0.0f;
			thoughtsPanel.SetActive(true);
		}

		IEnumerator CarbonInfo()
		{
			yield return new WaitForSeconds(2.0f);
			phase = Phase.Seven;
			textField.text = fifthText;
			Time.timeScale = 0.0f;
			thoughtsPanel.SetActive(true);
		}

		IEnumerator MultiplierInfo()
		{
			yield return new WaitForSeconds(2.0f);
			phase = Phase.Eight;
			textField.text = sixthText;
			Time.timeScale = 0.0f;
			carbonMeter.SetActive(true);
			carbonMultiplierText.SetActive(true);
			carbonMultiplierValue.SetActive(true);
			thoughtsPanel.SetActive(true);
		}

		public void OnOk()
		{
			thoughtsPanel.SetActive(false);

			switch (phase)
			{
				case Phase.Second:
					SecondThough();
					break;
				case Phase.Third:
					EnableGameplay();
					break;
				case Phase.Five:
					Time.timeScale = 1.0f;
					StartCoroutine(ObstacleAndBadFoodInfo());
					break;
				case Phase.Six:
					Time.timeScale = 1.0f;
					StartCoroutine(CarbonInfo());
					break;
				case Phase.Seven:
					Time.timeScale = 1.0f;
					StartCoroutine(MultiplierInfo());
					break;
				case Phase.Eight:
					Time.timeScale = 1.0f;
					GameManager.Instance.fixedGameSpeed = false;
					break;
			}
		}
	}
}
