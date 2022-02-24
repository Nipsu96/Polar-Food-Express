using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Polar
{
    public class ScoreManager : MonoBehaviour
    {
        // This holds data of current score of the run
        // We can pass this data to other places like UI or highscore panel

        internal static ScoreManager Instance { get; private set; }
        [ShowNonSerializedField] internal float currentScore;

        private void Awake()
        {
            Instance = this;
        }
    }
}