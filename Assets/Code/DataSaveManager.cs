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

		private void Awake()
		{
			CreateInstance();
			InitializeScoreFile();
		}

		// Debug purpose only
		private void OnEnable()
		{
			GameData.OnDataSave += OnSave;
		}

		// Debug purpose only
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

			// Check does save file exist and create one if it doesn't
			if (!System.IO.File.Exists(path))
			{
				Debug.Log("Save data doesn't exist. Creating a new one.");

				// Create new empty SaveDataObject
				CreateNewSaveDataObject(0, 0);

				// Save data to a drive
				SaveScore();
			}
		}

		public float GetHighscoreScoreData()
		{
			// Load sava data
			saveDataObject = GameData.LoadData(saveDataObject, path, false) as SaveDataObject;

			return saveDataObject.highscore;
		}

		public void SaveScoreData()
		{
			// Load sava data
			saveDataObject = GameData.LoadData(saveDataObject, path, false) as SaveDataObject;

			float latestScore = ScoreManager.Instance.currentScore;
			//Debug.Log("Latest score: " + latestScore);

			float highscore = saveDataObject.highscore;
			//Debug.Log("Current highscore: " + highscore);

			// Check is score high enough for the highscore list
			if (latestScore >= highscore)
			{
				highscore = latestScore;
				//Debug.Log("New highscore: " + highscore);
			}

			// Create new SaveDataObject with score values
			//saveDataObject = new SaveDataObject { latestScore = ScoreManager.Instance.currentScore, Highscore = highscore };
			saveDataObject = new SaveDataObject { latestScore = ScoreManager.Instance.currentScore, highscore = highscore, highscores = new float[] { 1, 2, 3 } };
			//CreateNewSaveDataObject(latestScore, highscore);

			// Save data to a drive
			SaveScore();
		}

		private void CreateNewSaveDataObject(float latestScore, float highscore)
		{
			saveDataObject = new SaveDataObject { latestScore = latestScore, highscore = highscore };
		}

		private void SaveScore()
		{
			GameData.SaveData(saveDataObject, path, false);
		}

		// Debug only
		private void OnSave()
		{
			Debug.Log("Saved successfully!");
		}
	}

	public class SaveDataObject
	{
		public float latestScore;
		public float highscore;
		public float[] highscores;

		// TODO: Highscore array for example, for the best 10 score values.

		//public float[] highscores = new float[5];
		//public float[] highscores = { 0, 0, 0, 0, 0 };

		//private float highscore;

		//public float Highscore
		//{
		//	get
		//	{
		//		return highscore;
		//	}
		//	set
		//	{
		//		highscore = value;
		//	}
		//}
	}
}
