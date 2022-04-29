using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Polar
{
    public class ButtonManager : MonoBehaviour
    {
        private string mainMenuName = "MainMenu";
        private string kauppahalliName = "Kauppahalli";
        private string optionsName = "OptionsMenu";
        private string scoreboardName = "ScoreboardMenu";
        private string creditsName = "CreditsMenu";
        private string tutorialName = "Tutorial";
        private GameObject creditsMenu;
        private GameObject mainMenu;
        private GameObject optionsMenu;
        private GameObject scoreboardMenu;
        private bool tutorialPlayed = false;
        [SerializeField] private GameObject tutorialButton;

        [SerializeField]
        private AudioControl musicControl;

        [SerializeField]
        private AudioControl sfxControl;

        [SerializeField]
        private Locale localeFin;
        [SerializeField]
        private Locale localeEN;
        private string locale;

        private void Start()
        {
            locale = PlayerPrefs.GetString("Locale");
            if (locale.Equals("English (en)"))
            {
                LocalizationSettings.SelectedLocale = localeEN;
            }
            else if (locale.Equals("Finnish (fi)"))
            {
                LocalizationSettings.SelectedLocale = localeFin;
            }
            

            FindMainMenu();
            FindOptionsMenu();
            FindCreditsMenu();
            FindScoreboardMenu();
            SetCorrectUIPanels();
            TutorialInitialize();
        }

        private void TutorialInitialize()
        {
            // Load from PlayerPrefs is tutorial already played.
            //tutorialPlayed = (PlayerPrefs.GetInt("tutorialPlayed") != 0);
            Debug.Log("Is tutorial done? (TutorialInitialize) " + tutorialPlayed);
            if (PlayerPrefs.HasKey("tutorialPlayed"))
            {
                Debug.Log("Tutorial key exists.");
            }
            else
            {
                Debug.Log("Tutorial key doesn't exist.");
            }
            tutorialPlayed = PlayerPrefs.GetInt("tutorialPlayed") == 1 ? true : false;
            Debug.Log("Is tutorial done? (after loading playerprefs) " + tutorialPlayed);

            if (tutorialPlayed)
            {
                tutorialButton.SetActive(true);
            }
            else
            {
                tutorialButton.SetActive(false);
            }
        }


        private void SetCorrectUIPanels()
        {
            if (!mainMenu.activeInHierarchy)
            {
                mainMenu.SetActive(true);
            }

            if (optionsMenu.activeInHierarchy)
            {
                optionsMenu.SetActive(false);
            }
            if (creditsMenu.activeInHierarchy)
            {
                creditsMenu.SetActive(false);
            }

            if (scoreboardMenu.activeInHierarchy)
            {
                scoreboardMenu.SetActive(false);
            }
        }

        public void OnPlayGame()
        {
            if (tutorialPlayed)
            {
                SceneManager.LoadSceneAsync(kauppahalliName);
            }
            else
            {
                tutorialPlayed = true;
                PlayerPrefs.SetInt("tutorialPlayed", (tutorialPlayed ? 1 : 0));
                SceneManager.LoadSceneAsync(tutorialName);
            }
        }
        public void OnOptions()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
        public void OnScoreboard()
        {
            mainMenu.SetActive(false);
            scoreboardMenu.SetActive(true);
        }
        public void OnCreditsMenu()
        {
            mainMenu.SetActive(false);
            creditsMenu.SetActive(true);
        }
        //         public void OnQuit()
        //         {
        //             Application.Quit();
        // #if UNITY_EDITOR
        //             UnityEditor.EditorApplication.isPlaying = false;
        // #endif
        //         }

        public void OnReplayTutorial()
        {
            SceneManager.LoadSceneAsync(tutorialName);
        }

        public void OnBackToMainMenu()
        {
            if (scoreboardMenu.activeInHierarchy)
            {
                scoreboardMenu.SetActive(false);
            }
            if (creditsMenu.activeInHierarchy)
            {
                creditsMenu.SetActive(false);
            }

            if (optionsMenu.activeInHierarchy)
            {
                optionsMenu.SetActive(false);
                musicControl.Save();
                sfxControl.Save();
            }
            mainMenu.SetActive(true);
        }

        private void FindMainMenu()
        {
            if (mainMenu == null)
            {
                mainMenu = transform.Find(mainMenuName).gameObject;
            }
        }

        private void FindOptionsMenu()
        {
            if (optionsMenu == null)
            {
                optionsMenu = transform.Find(optionsName).gameObject;
            }
        }

        private void FindScoreboardMenu()
        {
            if (scoreboardMenu == null)
            {
                scoreboardMenu = transform.Find(scoreboardName).gameObject;
            }
        }
        private void FindCreditsMenu()
        {
            if (creditsMenu == null)
            {
                creditsMenu = transform.Find(creditsName).gameObject;
            }
        }
    }
}