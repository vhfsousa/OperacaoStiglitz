using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetavelBalaThompson : MonoBehaviour
{
    [SerializeField] private GameObject jogador;
    [SerializeField] private int balasAdicionadas;
    [SerializeField] private AudioSource audioSourceBalaThompson;
    [SerializeField] private AudioClip somAudioThompson;

    void Awake()
    {
        jogador = GameObject.FindGameObjectWithTag("Player");
        audioSourceBalaThompson = this.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player")){
            jogador.GetComponent<SCPT_ArmasDoJogador>().municaoThompsonTotal += balasAdicionadas;
            audioSourceBalaThompson.PlayOneShot(somAudioThompson);
        }
        this.gameObject.SetActive(false);
    }
}