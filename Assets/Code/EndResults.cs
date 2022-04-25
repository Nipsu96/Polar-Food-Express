using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Polar
{
    public class EndResults : MonoBehaviour
    {
		[SerializeField] private TMP_Text collectedScoreValue;
		[SerializeField] private TMP_Text scoreMultiplierValue;
		[SerializeField] private TMP_Text totalScoreValue;
		[SerializeField] private TMP_Text bestScoreValue;
		private float collectedScore = 0.0f;
		private float scoreMultiplier = 0.0f;
		private float totalScore = 0.0f;
		private float bestScore = 0.0f;

		private void Start()
		{
			GetResults();
		}

		private void GetResults()
		{
			// Get latest collected score.
			collectedScore = DataSaveManager.Instance.GetLatestScoreData();
			collectedScoreValue.text = collectedScore.ToString();

			// Get latest score multiplier.
			scoreMultiplier = DataSaveManager.Instance.GetLatestMultiplierData();
			scoreMultiplierValue.text = scoreMultiplier.ToString() + "x";

			// Get latest total score (latest score * multiplier).
			totalScore = DataSaveManager.Instance.GetLatestTotalScoreData();
			totalScoreValue.text = totalScore.ToString();

			// Get the all time best score.
			bestScore = DataSaveManager.Instance.GetHighscoreData(0);
			bestScoreValue.text = bestScore.ToString();
		}
	}
}
