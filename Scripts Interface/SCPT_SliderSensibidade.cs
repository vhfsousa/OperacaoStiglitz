using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCPT_SliderSensibidade : MonoBehaviour
{
    [SerializeField] private float sensibilidadeGeral;
    [SerializeField] private Text textoSensibilidade;
    [SerializeField] private Slider sliderSensibilidade;

    void Start()
    {
        sensibilidadeGeral = PlayerPrefs.GetFloat("Sensibilidade");
        sliderSensibilidade.value = PlayerPrefs.GetFloat("Sensibilidade");
    }

    void Update()
    {
        textoSensibilidade.text = System.Math.Round(PlayerPrefs.GetFloat("Sensibilidade"), 2).ToString();
    }

    public void TrocarSensibilidade(float sensibilidadeSlider)
    {
        sensibilidadeGeral = sensibilidadeSlider;
        PlayerPrefs.SetFloat("Sensibilidade", sensibilidadeGeral);
    }
}