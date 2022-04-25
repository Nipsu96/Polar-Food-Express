using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Polar
{
    public class SetSFXVolume : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer mixer;
        public void SetLevel(float sliderValue)
        {
            mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);

        }
    }
}
