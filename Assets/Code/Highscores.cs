using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Polar
{
    public class Highscores : MonoBehaviour
    {
		[SerializeField] private TMP_Text highscore;

		private void Start()
		{
			highscore.text = $"1. { DataSaveManager.Instance.GetHighscoreScoreData() } points";
		}
	}
}
