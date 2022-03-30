using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

namespace Polar
{
    public class VersionManager : MonoBehaviour
    {
        internal static VersionManager Instance { get; private set; }
        private TMP_Text versionNumberUI;
        private string textVersionNumber = "VersionNumber";

        private void Awake()
        {
            CreateInstance();
        }

        private void Start()
        {
            FindScoreValue();
        }

        private void FindScoreValue()
        {
            if (versionNumberUI == null)
            {
                versionNumberUI = GameObject.Find(textVersionNumber).GetComponent<TMP_Text>();
            }
            versionNumberUI.text = " Version: " + Application.version;
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
