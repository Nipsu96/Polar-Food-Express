using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DataManager;

namespace Polar
{
    public class DataSaveManager : MonoBehaviour
    {
		public SaveDataObject saveDataObject;
		[SerializeField] private float[] highscore = { 100.0f, 90.0f, 80.0f, 70.0f, 60.0f, 50.0f };
		[ShowNonSerializedField] private float latestScore = 10.0f;
		private string path;

		private void Start()
		{
			GameData.OnDataSave += OnSave;
			path = Application.persistentDataPath + "/Save.dat";
			saveDataObject = new SaveDataObject();
			saveDataObject = GameData.LoadData(saveDataObject, path) as SaveDataObject;
		}

		private void OnSave()
		{
			print("Saved successfully!");
		}
	}

	public class SaveDataObject
	{
		public float score = 20.0f;
	}
}
