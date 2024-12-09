using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudoConfig : MonoBehaviour
{
    [SerializeField] AudioMixer AudioMixer;
    [SerializeField] AudioSource Sellect_Sound;
    [SerializeField] AudioSource Title_BGM;
    [SerializeField] Slider SE_Slider;
    [SerializeField] Slider BGM_Slider;
    // Start is called before the first frame update
    private void Start()
    {
        BGM_Slider.onValueChanged.AddListener((value) =>
        {
            value = Mathf.Clamp01(value);

            float dB = 20f * Mathf.Log10(value);
            dB = Mathf.Clamp(dB, -80f, 0f);
            AudioMixer.SetFloat("BGM", dB);
        });

        SE_Slider.onValueChanged.AddListener((value) =>
        {
            value = Mathf.Clamp01(value);

            float dB = 20f * Mathf.Log10(value);
            dB = Mathf.Clamp(dB, -80f, 0f);
            AudioMixer.SetFloat("SE", dB);
        });
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
