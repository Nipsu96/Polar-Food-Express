using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

namespace Polar
{
    public class GameManager : MonoBehaviour
	{
		[SerializeField] internal bool fixedGameSpeed = false;
		internal static GameManager Instance { get; private set; }
        [SerializeField, Range(0.0f, 1000.0f), Tooltip("This controls the speed of all moving foods, obstacles and backgrounds")]
		internal float gameSpeed = 1.0f;
		[SerializeField, Range(1.0f, 100.0f), Tooltip("Low number = game speeds up faster, high number = game speeds up slower"), DisableIf("fixedGameSpeed")]
		private float gameSpeedThrottle = 10.0f;
		internal delegate void EndAction();
		internal static event EndAction GameOver;
		internal bool isTutorialOver = true;

        private void Awake()
        {
            CreateInstance();
			Time.timeScale = 1.0f;
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
			isTutorialOver = TutorialManager.Instance.tutorialCompleted;
		}

		internal void EndGame()
		{
			// Save current score data to a file
			DataSaveManager.Instance.SaveScoreData();
			gameSpeed = 0;

			Time.timeScale = 1.0f;

			// Start GameOver event. Other objects will listen this and disable themselves.
			GameOver();

			Scene currentScene = SceneManager.GetActiveScene();
			if (currentScene.name == "Tutorial")
			{
				isTutorialOver = TutorialManager.Instance.tutorialCompleted;
			}

			if (isTutorialOver)
			{
				Scene scene = SceneManager.GetActiveScene();
				String sceneName = scene.name;
				SceneManager.LoadScene("LoseScreen", LoadSceneMode.Additive);
			}
			else
			{
				Time.timeScale = 0;
				TutorialManager.Instance.retryTutorialMenu.SetActive(true);
			}
		}

		private void FixedUpdate()
		{
			if (!fixedGameSpeed)
			{
				gameSpeed += Time.deltaTime / gameSpeedThrottle;
			}
		}
	}
}