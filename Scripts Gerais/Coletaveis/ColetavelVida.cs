using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jogador;

public class ColetavelVida : MonoBehaviour
{
    [SerializeField] private GameObject jogador;
    [SerializeField] private int vidaAdicionada;
    [SerializeField] private AudioSource audioSourceVida;
    [SerializeField] private AudioClip somAudioVida;

    void Awake()
    {
        jogador = GameObject.FindGameObjectWithTag("Player");
        audioSourceVida = this.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player")){
            jogador.GetComponent<SCPT_Jogador>().vidaJogador += vidaAdicionada;
            audioSourceVida.PlayOneShot(somAudioVida);
        }
        this.gameObject.SetActive(false);
    }
}