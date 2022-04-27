using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Polar
{
    public class Highscores : MonoBehaviour
    {
		[SerializeField] private GameObject textPrefab;
		[SerializeField] private TMP_Text[] scores;
		[SerializeField] private TMP_FontAsset font;
		private int maxHighscores;

		private void Start()
		{
			maxHighscores = DataSaveManager.Instance.maxHighscores;
			scores = new TMP_Text[maxHighscores];
			//highscore.text = $"1. { DataSaveManager.Instance.GetHighscoreScoreData() } points";

			for (int i = 0; i < maxHighscores; i++)
			{
				scores[i] = InstantiateScoreText(i);
			}

			for (int i = 0; i < maxHighscores; i++)
			{
				scores[i].text = $"{ (i + 1) }. { DataSaveManager.Instance.GetHighscoreData(i) } points";
			}
		}

		private TMP_Text InstantiateScoreText(int index)
		{
			TMP_Text score = Instantiate(textPrefab.GetComponent<TMP_Text>(), transform.position, transform.rotation);
			score.font = font;
			score.transform.SetParent(gameObject.transform);
			score.transform.localScale = new Vector3(1, 1, 1);
			return score;
		}
	}
}
