using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Polar
{
    public class TutorialManager : MonoBehaviour
	{
		// NOTE: Don't look here. This was made in the middle of the night while crunching.

		internal static TutorialManager Instance { get; private set; }
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
		private PlayerController playerController;
		[SerializeField] private Button pauseButton;
		internal bool tutorialCompleted;
		[SerializeField] internal GameObject retryTutorialMenu;
		[SerializeField] private GameObject canvas;
		internal delegate void StartGameplay();
		internal static event StartGameplay StartGameplayLoop;

		[Header("Texts")]
		[SerializeField] private LocalizedString firstText;
		[SerializeField] private LocalizedString secondText;
		[SerializeField] private LocalizedString thirdText;
		[SerializeField] private LocalizedString fourthText;
		[SerializeField] private LocalizedString fifthText;
		[SerializeField] private LocalizedString sixthText;

		private void Awake()
		{
			CreateInstance();
		}

		private void CreateInstance()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

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
				Vector3 dif = bearPosition.transform.position - rbPolarBear.transform.position;
				if (dif.magnitude > 1)
				{
					rbPolarBear.AddForce(dif * multiplier * Time.deltaTime);
				}
				else
				{
					rbPolarBear.velocity = Vector2.zero;
					animator.SetBool("idle", true);
					phase = Phase.Second;
				}
			}

			if (phase == Phase.Second)
			{
				playerController.DisableControls();
				pauseButton.enabled = false;
				thoughtsPanel.SetActive(true);
				textField.text = firstText.GetLocalizedString();
			}
		}

		private void InitializeSettings()
		{
			playerController = playerCharacter.GetComponent<PlayerController>();
			phase = Phase.Beginning;
			GameManager.Instance.fixedGameSpeed = true;
			GameManager.Instance.gameSpeed = 2;
			objectSpawner.enableSpawning = false;
			playerController.DisableControls();
			rbPolarBear = playerCharacter.GetComponent<Rigidbody2D>();
			animator = playerCharacter.GetComponent<Animator>();
			animator.enabled = false;
			pauseButton.enabled = true;
			thoughtsPanel.SetActive(false);
			canvas.GetComponent<DisableObjectOnEvent>().enabled = false;
		}

		IEnumerator WaitAtStart()
		{
			yield return new WaitForSeconds(waitTimeAtStart);
			phase = Phase.First;
			animator.enabled = true;
		}

		private void SecondThough()
		{
			phase = Phase.Third;
			textField.text = secondText.GetLocalizedString();
			playerController.DisableControls();
			pauseButton.enabled = false;
			thoughtsPanel.SetActive(true);
		}

		private void EnableGameplay()
		{
			phase = Phase.Four;

			StartGameplayLoop();
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
			textField.text = thirdText.GetLocalizedString();
			Time.timeScale = 0.0f;
			playerController.DisableControls();
			pauseButton.enabled = false;
			thoughtsPanel.SetActive(true);
		}

		IEnumerator ObstacleAndBadFoodInfo()
		{
			yield return new WaitForSeconds(2.0f);
			phase = Phase.Six;
			textField.text = fourthText.GetLocalizedString();
			Time.timeScale = 0.0f;
			playerController.DisableControls();
			pauseButton.enabled = false;
			thoughtsPanel.SetActive(true);
		}

		IEnumerator CarbonInfo()
		{
			yield return new WaitForSeconds(2.0f);
			phase = Phase.Seven;
			textField.text = fifthText.GetLocalizedString();
			Time.timeScale = 0.0f;
			playerController.DisableControls();
			pauseButton.enabled = false;
			thoughtsPanel.SetActive(true);
		}

		IEnumerator MultiplierInfo()
		{
			yield return new WaitForSeconds(2.0f);
			phase = Phase.Eight;
			textField.text = sixthText.GetLocalizedString();
			Time.timeScale = 0.0f;
			playerController.DisableControls();
			pauseButton.enabled = false;
			thoughtsPanel.SetActive(true);
		}

		public void OnOk()
		{
			thoughtsPanel.SetActive(false);
			playerController.EnableControls();
			pauseButton.enabled = true;

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
					canvas.GetComponent<DisableObjectOnEvent>().enabled = true;
					tutorialCompleted = true;
					break;
			}
		}
	}
}
