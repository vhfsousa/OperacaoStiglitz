using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jogador;

public class SCPT_MenuInGame : MonoBehaviour
{
    [SerializeField] private GameObject menuInGame;
    [SerializeField] private GameObject interfaceDeMorte;
    [SerializeField] private GameObject jogador;
    [SerializeField] private GameObject camerasCinemachinePadrao;
    [SerializeField] private GameObject camerasCinemachineMira;

    void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {
        if(menuInGame.activeInHierarchy == true)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            jogador.GetComponent<SCPT_ArmasDoJogador>().enabled = false;
            camerasCinemachinePadrao.SetActive(false);
            camerasCinemachineMira.SetActive(false);
            if(Input.GetKeyDown(KeyCode.Escape)){
                menuInGame.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            jogador.GetComponent<SCPT_ArmasDoJogador>().enabled = true;
            camerasCinemachinePadrao.SetActive(true);
            camerasCinemachineMira.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Escape)){
                menuInGame.SetActive(true);
            }
        }

        if(interfaceDeMorte.activeInHierarchy == true){
            Time.timeScale = 0.25f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            jogador.GetComponent<SCPT_ArmasDoJogador>().enabled = false;
            jogador.GetComponent<SCPT_Jogador>().enabled = false;
            camerasCinemachinePadrao.SetActive(false);
            camerasCinemachineMira.SetActive(false);
        }
    }
}