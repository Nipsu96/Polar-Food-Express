using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

namespace Polar
{
    public class ScoreManager : MonoBehaviour
    {
        // This holds data of current score of the run
        // We can pass this data to other places like UI or highscore panel

        internal static ScoreManager Instance { get; private set; }
        [ShowNonSerializedField] internal float currentScore;
        [ShowNonSerializedField] private float score;
        [SerializeField] private TMP_Text scoreUI;

        private void Awake()
        {
            Instance = this;
        }

        public void SetScore(float score)
        {
            this.score += score;
            GetScoreForUI();
        }

        private void GetScoreForUI()
        {
            scoreUI.SetText(score.ToString());
        }
    }
}