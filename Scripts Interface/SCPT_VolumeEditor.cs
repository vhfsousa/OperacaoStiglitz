using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCPT_VolumeEditor : MonoBehaviour
{
    [SerializeField] private float volumeGeral;
    [SerializeField] private Text textoVolume;
    [SerializeField] private Slider sliderVolume;

    void Start()
    {
        volumeGeral = PlayerPrefs.GetFloat("Volume");
        sliderVolume.value = PlayerPrefs.GetFloat("Volume");
    }

    void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        textoVolume.text  = System.Math.Round((PlayerPrefs.GetFloat("Volume") * 100)).ToString() + "%";
    }

    public void TrocarVolume(float volumeSlider)
    {
        volumeGeral = volumeSlider;
        PlayerPrefs.SetFloat("Volume", volumeGeral);
    }
}