using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DataManager;
using System;

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

		private void Awake()
		{
			CreateInstance();
			InitializeScoreFile();
		}

		// Debug purpose only.
		private void OnEnable()
		{
			GameData.OnDataSave += OnSave;
		}

		// Debug purpose only.
		private void OnDisable()
		{
			GameData.OnDataSave -= OnSave;
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

		private void InitializeScoreFile()
		{
			// Set path for the save data file.
			path = Application.persistentDataPath + "/ScoreData.dat";

			// Check does save file exist and create one if it doesn't.
			if (!System.IO.File.Exists(path))
			{
				Debug.Log("Save data doesn't exist. Creating a new one.");

				// Create new empty SaveDataObject.
				SaveDataObject(emptyInitialScore, emptyInitialMultiplier, new float[maxHighscores]);

				// Save data to a drive.
				SaveScore();
			}
		}

		//public float GetHighscoreScoreData()
		//{
		//	// Load sava data.
		//	saveDataObject = GameData.LoadData(saveDataObject, path, false) as SaveDataObject;

		//	return saveDataObject.highscore;
		//}

		public float GetHighscoreData(int index)
		{
			// Load sava data.
			saveDataObject = GameData.LoadData(saveDataObject, path, false) as SaveDataObject;

			return saveDataObject.highscores[index];
		}

		public void SaveScoreData()
		{
			// Load sava data.
			saveDataObject = GameData.LoadData(saveDataObject, path, false) as SaveDataObject;

			float latestScore = ScoreManager.Instance.currentScore;
			float latestScoreMultiplier = CarbonManager.Instance.currentCarbonFootprint;
			float[] highscores = saveDataObject.highscores;

			// Check is the latest score high enough for the highscore list.
			highscores = UpdateHighscores(latestScore, highscores);

			// Update SaveDataObject with score and multiplier values.
			SaveDataObject(latestScore, latestScoreMultiplier, highscores);

			// Save data to a drive.
			SaveScore();
		}

		private float[] UpdateHighscores(float latestScore, float[] highscores)
		{
			int index = 0;

			// Check is the lastest bigger or equal than any of the highscores.
			for (int i = 0; i < maxHighscores; i++)
			{
				if(latestScore >= highscores[i])
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
				highscores[index] = latestScore;
			}

			return highscores;
		}

		private void SaveDataObject(float latestScore, float latestScoreMultiplier, float[] highscores)
		{
			saveDataObject = new SaveDataObject { latestScore = latestScore, latestScoreMultiplier = latestScoreMultiplier, highscores = highscores };
		}

		private void SaveScore()
		{
			GameData.SaveData(saveDataObject, path, false);
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
		public float[] highscores;
	}
}
