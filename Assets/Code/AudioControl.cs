using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Polar
{
    public class AudioControl : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
		[SerializeField] private Slider slider;

		[SerializeField] private string volumeName;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
        }

        public void Setup(AudioMixer mixer, string volumeName)
        {
            this.mixer = mixer;
            this.volumeName = volumeName;

            if (mixer.GetFloat(volumeName, out float volume))
            {
                slider.value = ToLinear(volume);

            }
            mixer.SetFloat(volumeName, ToDB(slider.value));
            slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });

			print(volumeName + "'s value on setup: " + slider.value);
        }
        public void ValueChangeCheck()
        {
            Debug.Log(slider.value);
        }
        private float ToDB(float linear)
        {
            return linear <= 0 ? -80f : Mathf.Log10(linear) * 20;
        }
        private float ToLinear(float db)
        {
            return Mathf.Clamp01(Mathf.Pow(10.0f, db / 20.0f));
        }

        public void Save()
        {
			print("Save: " + volumeName + "'s value: " + slider.value);

            mixer.SetFloat(volumeName, ToDB(slider.value));
        }
    }
}
