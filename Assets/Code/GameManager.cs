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
		[SerializeField] private bool fixedGameSpeed = false;
		internal static GameManager Instance { get; private set; }
        [SerializeField, Range(0.0f, 1000.0f), Tooltip("This controls the speed of all moving foods, obstacles and backgrounds")]
		internal float gameSpeed = 1.0f;
		[SerializeField, Range(1.0f, 100.0f), Tooltip("Low number = game speeds up faster, high number = game speeds up slower"), DisableIf("fixedGameSpeed")]
		private float gameSpeedThrottle = 10.0f;
		internal delegate void EndAction();
		internal static event EndAction GameOver;

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

        internal void EndGame()
		{
			// Save current score data to a file
			DataSaveManager.Instance.SaveScoreData();

			Scene scene = SceneManager.GetActiveScene();
            String sceneName = scene.name;
			SceneManager.LoadScene("LoseScreen", LoadSceneMode.Additive);
			gameSpeed = 0;

			// Start GameOver event. Other objects will listen this and disable themselves.
			GameOver();
		}

		private void FixedUpdate()
		{
			if (!fixedGameSpeed)
			{
				gameSpeed += Time.deltaTime / gameSpeedThrottle;
			}
			else
			{

			}
		}
	}
}