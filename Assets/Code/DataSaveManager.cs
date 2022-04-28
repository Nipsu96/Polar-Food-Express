using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DataManager;
using System;
using UnityEditor;

namespace Polar
{
    public class DataSaveManager : MonoBehaviour
    {
		internal static DataSaveManager Instance { get; private set; }
		public SaveDataObject saveDataObject;
		private string path;
		[SerializeField] internal int maxHighscores = 12;
		private float emptyInitialScore = 0.0f;
		private float emptyInitialMultiplier = 0.0f;
		private float emptyInitialTotal = 0.0f;
		private bool encryptSaveFile = true;
		[SerializeField] private bool hasLaunchedBefore = false;

		private void Awake()
		{
			CreateInstance();
			SetFirstTimeLaunch();
			InitializeScoreFile();
		}

		// Debug purpose only.
		private void OnEnable()
		{
			//GameData.OnDataSave += OnSave;
		}

		// Debug purpose only.
		private void OnDisable()
		{
			//GameData.OnDataSave -= OnSave;
		}

		private void CreateInstance()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(this);
			}
		}

		private void SetFirstTimeLaunch()
		{
			// Get boolean using PlayerPrefs

			print("hasLaunchedBefore 1 : " + hasLaunchedBefore);
			hasLaunchedBefore = PlayerPrefs.GetInt("hasLaunchedBefore") == 1 ? true : false;
			print("hasLaunchedBefore 2 : " + hasLaunchedBefore);
			//PlayerPrefs.SetInt("firstTimeLaunch", (firstTimeLaunch ? 1 : 0));
			//print("firstTimeLaunch 3 : " + firstTimeLaunch);
			//firstTimeLaunch = (PlayerPrefs.GetInt("firstTimeLaunch") != 0);
		}

		private void InitializeScoreFile()
		{
			// Set path for the save data file.
			path = Application.persistentDataPath + "/ScoreData.dat";

			if (!hasLaunchedBefore)
			{
				print("Launching game for the first time");
				FileUtil.DeleteFileOrDirectory(path);
				hasLaunchedBefore = true;
				PlayerPrefs.SetInt("hasLaunchedBefore", (hasLaunchedBefore ? 1 : 0));
				path = Application.persistentDataPath + "/ScoreData.dat";
			}

			// Check does save file exist and create one if it doesn't.
			if (!System.IO.File.Exists(path))
			{
				Debug.Log("Save data doesn't exist. Creating a new one.");

				// Create new empty SaveDataObject.
				SaveDataObject(emptyInitialScore, emptyInitialTotal, emptyInitialMultiplier, new float[maxHighscores]);

				// Save data to a drive.
				SaveScore();
			}
		}

		public float GetHighscoreData(int index)
		{
			LoadSaveData();
			return saveDataObject.highscores[index];
		}

		public float GetLatestScoreData()
		{
			LoadSaveData();
			return saveDataObject.latestScore;
		}

		public float GetLatestMultiplierData()
		{
			LoadSaveData();
			return saveDataObject.latestScoreMultiplier;
		}

		public float GetLatestTotalScoreData()
		{
			LoadSaveData();
			return saveDataObject.latestTotalScore;
		}

		public void SaveScoreData()
		{
			LoadSaveData();

			float latestScore = ScoreManager.Instance.currentScore;

			float latestScoreMultiplier = CarbonManager.Instance.currentCarbonFootprint;

			float latestTotalScore = 0.0f;
			latestTotalScore = CalculateTotalScore(latestScore, latestScoreMultiplier);

			float[] highscores = saveDataObject.highscores;


			// Check is the latest score high enough for the highscore list.
			highscores = UpdateHighscores(latestTotalScore, highscores);

			// Update SaveDataObject with score and multiplier values.
			SaveDataObject(latestScore, latestScoreMultiplier, latestTotalScore, highscores);

			// Save data to a drive.
			SaveScore();
		}

		private void LoadSaveData()
		{
			saveDataObject = GameData.LoadData(saveDataObject, path, encryptSaveFile) as SaveDataObject;
		}

		private float CalculateTotalScore(float latestScore, float latestScoreMultiplier)
		{
			float totalScore = 0.0f;
			totalScore = Mathf.Round(latestScore * latestScoreMultiplier);
			return totalScore;
		}

		private float[] UpdateHighscores(float latestTotalScore, float[] highscores)
		{
			int index = 0;

			// Check is the lastest bigger or equal than any of the highscores.
			for (int i = 0; i < maxHighscores; i++)
			{
				if(latestTotalScore >= highscores[i])
				{
					// If the lastest score is bigger or equal than any, break.
					break;
				}
				index++;
			}

			// The latest score is bigger or equal -> update highscores.
			if(index < maxHighscores)
			{
				for (int i = (maxHighscores - 1); i > index; i--)
				{
					highscores[i] = highscores[i - 1];
				}
				highscores[index] = latestTotalScore;
			}

			return highscores;
		}

		private void SaveDataObject(float latestScore, float latestScoreMultiplier, float latestTotalScore, float[] highscores)
		{
			saveDataObject = new SaveDataObject
			{
				latestScore = latestScore,
				latestScoreMultiplier = latestScoreMultiplier,
				latestTotalScore = latestTotalScore,
				highscores = highscores
			};
		}

		private void SaveScore()
		{
			GameData.SaveData(saveDataObject, path, encryptSaveFile);
		}

		// Debug only.
		private void OnSave()
		{
			Debug.Log("Saved successfully!");
		}
	}

	public class SaveDataObject
	{
		public float latestScore;
		public float latestScoreMultiplier;
		public float latestTotalScore;
		public float[] highscores;
	}
}
