using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polar
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] internal int score;
        [SerializeField, Tooltip("Set a positive value to good products and a negative value to bad products.")] internal float carbonFootprintImpact;
    }
}
