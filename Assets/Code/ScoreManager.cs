using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


namespace Polar
{
    public class ScoreManager : MonoBehaviour
    {
        // This holds data of current score of the run
        // We can pass this data to other places like UI or highscore panel

        internal static ScoreManager Instance { get; private set; }
		[SerializeField] private bool multiplyScoreDuringRun = true;
        internal float currentScore;
        private TMP_Text scoreValueUI;
        private string textScoreValue = "Text_ScoreValue";

        [SerializeField]
        private LocalizedString localizedVersion;



        private void Awake()
        {
            CreateInstance();
        }

        private void Start()
        {
            FindScoreValue();
        }
        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }

        private void OnLocaleChanged(Locale obj)
        {
            FindScoreValue();
        }
        private void FindScoreValue()
        {
            if (scoreValueUI == null)
            {
                scoreValueUI = GameObject.Find(textScoreValue).GetComponent<TMP_Text>();
            }
            scoreValueUI.text = string.Format(localizedVersion.GetLocalizedString(), currentScore);
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

        internal void AddScore(float score)
        {
            this.currentScore += score;
            scoreValueUI.text = string.Format(localizedVersion.GetLocalizedString(), currentScore);
        }
    }
}