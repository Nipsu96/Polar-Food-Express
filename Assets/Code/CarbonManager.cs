using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Polar
{
    public class CarbonManager : MonoBehaviour
    {
        // This holds data of current carbon footprint of the run
        // We can pass this data to other places like UI or highscore panel
        internal static CarbonManager Instance { get; private set; }
        [ShowNonSerializedField] internal float currentCarbonFootprint;

        private void Awake()
        {
            Instance = this;
        }
    }
}
