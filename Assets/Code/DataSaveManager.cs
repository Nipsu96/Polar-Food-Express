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
		[SerializeField] internal int maxHighscores = 10;

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
				CreateNewSaveDataObject(0, 0, new float[maxHighscores]);

				// Save data to a drive.
				SaveScore();
			}
		}

		public float GetHighscoreScoreData()
		{
			// Load sava data.
			saveDataObject = GameData.LoadData(saveDataObject, path, false) as SaveDataObject;

			return saveDataObject.highscore;
		}

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
			//Debug.Log("Latest score: " + latestScore);

			float highscore = saveDataObject.highscore;
			//Debug.Log("Current highscore: " + highscore);

			float[] highscores = saveDataObject.highscores;

			highscores = UpdateHighscores(latestScore, highscores);

			// Check is score high enough for the highscore list
			if (latestScore >= highscore)
			{
				highscore = latestScore;
				//Debug.Log("New highscore: " + highscore);
			}

			// Create new SaveDataObject with score values
			//saveDataObject = new SaveDataObject { latestScore = ScoreManager.Instance.currentScore, Highscore = highscore };
			saveDataObject = new SaveDataObject { latestScore = ScoreManager.Instance.currentScore, highscore = highscore, highscores = highscores };
			//CreateNewSaveDataObject(latestScore, highscore);

			// Save data to a drive.
			SaveScore();
		}

		private float[] UpdateHighscores(float latestScore, float[] highscores)
		{
			//float[] highscores = new float[maxHighscores];
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

			//highscores = new float[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

			return highscores;
		}

		private void CreateNewSaveDataObject(float latestScore, float highscore, float[] highscores)
		{
			saveDataObject = new SaveDataObject { latestScore = latestScore, highscore = highscore, highscores = new float[maxHighscores] };
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
