using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCPT_Medidor : MonoBehaviour
{
    public int nivelMedidor;
    [SerializeField] private bool tocouSom;
    [SerializeField] private Image ponteiroMedidor;
    [SerializeField] private float distanciaPonteiro;
    [SerializeField] private AudioSource audioMedidor;
    [SerializeField] private AudioClip somMudarMedidor;

    void Start()
    {
        nivelMedidor = 0;
        tocouSom = false;
    }

    void Update()
    {
        if(nivelMedidor > 50){
            nivelMedidor = 50;
        }
        
        //Mexer na interface do medidor
        ponteiroMedidor.rectTransform.eulerAngles = new Vector3 (0, 0, (82f - (nivelMedidor * 3.25f)));

        /*Se o jogador for visto
        {
            nivelMedidor += 1;
        }*/
        
        //Colocar um switch para os níveis aqui e fazer também com que o medidor troque de cor
        switch (nivelMedidor)
        {
            case 10:
                if(tocouSom == false){
                    audioMedidor.PlayOneShot(somMudarMedidor);
                    tocouSom = true;
                }
            break;

            case 20:
                if(tocouSom == false){
                    audioMedidor.PlayOneShot(somMudarMedidor);
                    tocouSom = true;
                }
            break;

            case 30:
                if(tocouSom == false){
                    audioMedidor.PlayOneShot(somMudarMedidor);
                    tocouSom = true;
                }
            break;

            case 40:
                if(tocouSom == false){
                    audioMedidor.PlayOneShot(somMudarMedidor);
                    tocouSom = true;
                }
            break;

            case 50:
                if(tocouSom == false){
                    audioMedidor.PlayOneShot(somMudarMedidor);
                    tocouSom = true;
                }
            break;

            default:
                tocouSom = false;
            break;
        }

        Scene actualScene = SceneManager.GetActiveScene();

        if(actualScene.name == "Fase3")
        {
            nivelMedidor = 50;
        }
    }
}