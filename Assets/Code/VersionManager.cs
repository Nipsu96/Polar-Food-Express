using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Polar
{
    public class VersionManager : MonoBehaviour
    {
        internal static VersionManager Instance { get; private set; }
        private TMP_Text versionNumberUI;
        private string textVersionNumber = "VersionNumber";

        // localization stuff
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

        private void OnEnable() {
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        }

        private void OnDisable() {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }

        private void OnLocaleChanged(Locale obj){
            FindScoreValue();
        }

        private void FindScoreValue()
        {
            if (versionNumberUI == null)
            {
                versionNumberUI = GameObject.Find(textVersionNumber).GetComponent<TMP_Text>();
            }
            versionNumberUI.text = localizedVersion.GetLocalizedString() + Application.version;
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
    }
}
