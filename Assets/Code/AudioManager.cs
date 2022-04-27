using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Polar
{
    public class AudioManager : MonoBehaviour
    {
		internal static AudioManager Instance { get; private set; }
		internal float musicLevel;
		internal float sfxLevel;
		[SerializeField] private AudioMixer mixer;

		void Awake()
		{
			CreateInstance();
		}

		private void Start()
		{
			musicLevel = PlayerPrefs.GetFloat("MusicVolume", 1);
			mixer.SetFloat("MusicVolume", Mathf.Log10(musicLevel) * 20);

			sfxLevel = PlayerPrefs.GetFloat("SFXVolume", 1);
			mixer.SetFloat("SFXVolume", Mathf.Log10(sfxLevel) * 20);
		}

		public void SetLevel(float sliderValue, string mixerGroup)
		{
			mixer.SetFloat(mixerGroup, Mathf.Log10(sliderValue) * 20);
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
	}
}
