using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jogador;

public class SCPT_BalaInimigo : MonoBehaviour
{
    void Update()
    {
        Destroy(this.gameObject, 20f);
    }

    void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SCPT_Jogador>().vidaJogador -= 10;
            if(col.gameObject.GetComponent<SCPT_Jogador>().vidaJogador >= 0)
            {
                col.gameObject.GetComponent<SCPT_Jogador>().StartCoroutine("PiscarTela");
            }
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}