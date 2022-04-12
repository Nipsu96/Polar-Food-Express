using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;
using System;

namespace Polar
{
    public class CarbonManager : MonoBehaviour
    {
        // This holds data of current carbon footprint of the run
        // We can pass this data to other places like UI or highscore panel

        internal static CarbonManager Instance { get; private set; }
		[SerializeField] private float carbonMinValue = -5.0f;
		[SerializeField] private float carbonMaxValue = 5.0f;
		[SerializeField] private float startingValue = 1.0f;
        [SerializeField] internal float currentCarbonFootprint;
        private string textCarbonMeter = "CarbonMeter";
        private string textScoreMultiplier = "Text_ScoreMultiplierValue";
        private Slider carbonMeter;
        private TMP_Text scoreMultiplierUI;

		private void Awake()
        {
            CreateInstance();
        }

        private void Start()
        {
            FindCarbonMeter();
            FindScoreMultiplier();
			SetStartingValues();
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

		private void FindCarbonMeter()
        {
            if (carbonMeter == null)
            {
                carbonMeter = GameObject.Find(textCarbonMeter).GetComponent<Slider>();
            }
        }

        private void FindScoreMultiplier()
        {
            if (scoreMultiplierUI == null)
            {
                scoreMultiplierUI = GameObject.Find(textScoreMultiplier).GetComponent<TMP_Text>();
            }
		}

		private void SetStartingValues()
		{
			carbonMeter.minValue = carbonMinValue;
			carbonMeter.maxValue = carbonMaxValue;
			currentCarbonFootprint = startingValue;
			carbonMeter.value = currentCarbonFootprint;
			scoreMultiplierUI.text = currentCarbonFootprint.ToString();
		}

        internal void AddCarbon(float carbonChange)
        {
			// Change carbon footprint value by collected food's carbon footprint
            this.currentCarbonFootprint -= carbonChange;

			// Check and keep currentCarbonFootprint value in min-max range
			if (currentCarbonFootprint >= carbonMaxValue)
			{
				currentCarbonFootprint = carbonMaxValue;
			}
			else if(currentCarbonFootprint <= carbonMinValue)
			{
				currentCarbonFootprint = carbonMinValue;
				GameManager.Instance.EndGame();
			}

			// Update carbon footprint UI slider element
			carbonMeter.value = currentCarbonFootprint;

            // Update score multiplier UI element
            scoreMultiplierUI.text = currentCarbonFootprint.ToString();
        }
    }
}