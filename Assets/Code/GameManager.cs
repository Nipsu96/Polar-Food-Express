using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Polar
{
    public class GameManager : MonoBehaviour
    {
        internal static GameManager Instance { get; private set; }
        [SerializeField] private SOGameSpeed gameSpeed;
        [SerializeField, Range(0.0f, 1000.0f)] private float speed = 1.0f;

        private void Awake()
        {
            CreateInstance();
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