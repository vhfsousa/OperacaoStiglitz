using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetavelBalaP08 : MonoBehaviour
{
    [SerializeField] private GameObject jogador;
    [SerializeField] private int balasAdicionadas;
    [SerializeField] private AudioSource audioSouceBalaP08;
    [SerializeField] private AudioClip somAudioP08;

    void Awake()
    {
        jogador = GameObject.FindGameObjectWithTag("Player");
        audioSouceBalaP08 = this.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player")){
            jogador.GetComponent<SCPT_ArmasDoJogador>().municaoP08Total += balasAdicionadas;
            audioSouceBalaP08.PlayOneShot(somAudioP08);
        }
        this.gameObject.SetActive(false);
    }
}