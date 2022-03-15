using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;

namespace Polar
{
    public class CarbonManager : MonoBehaviour
    {
        // This holds data of current carbon footprint of the run
        // We can pass this data to other places like UI or highscore panel

        internal static CarbonManager Instance { get; private set; }
        private float currentCarbonFootprint;
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
        }

        private void FindCarbonMeter()
        {
            if (carbonMeter == null)
            {
                carbonMeter = GameObject.Find(textCarbonMeter).GetComponent<Slider>();
            }
            carbonMeter.value = currentCarbonFootprint;
        }

        private void FindScoreMultiplier()
        {
            if (scoreMultiplierUI == null)
            {
                scoreMultiplierUI = GameObject.Find(textScoreMultiplier).GetComponent<TMP_Text>();
            }
            scoreMultiplierUI.text = currentCarbonFootprint.ToString();
        }

        private void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        internal void AddCarbon(float carbonChange)
        {
            this.currentCarbonFootprint += carbonChange;
            // Update carbon footprint UI slider element
            carbonMeter.value = currentCarbonFootprint;
            // Update score multiplier UI element
            scoreMultiplierUI.text = currentCarbonFootprint.ToString();
        }
    }
}