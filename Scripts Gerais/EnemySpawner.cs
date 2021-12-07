using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private bool podeSpawnar;
    [SerializeField] private List<Transform> spawns;
    [SerializeField] private List<GameObject> inimigos;
    [SerializeField] private GameObject medidor;

    void Awake(){
        podeSpawnar = false;
        medidor = FindObjectOfType<SCPT_Medidor>().gameObject;
    }

    void Start()
    {
    }

    void Update()
    {
        switch (medidor.GetComponent<SCPT_Medidor>().nivelMedidor)
        {
            case 10:
                if(podeSpawnar == false){
                    for (int i = 0; i < 1; i++)
                    {
                        inimigos[i].SetActive(true);
                    }
                    podeSpawnar = true;
                }
            break;

            case 20:
                if(podeSpawnar == false){
                    for (int i = 1; i < 3; i++)
                    {
                        inimigos[i].SetActive(true);
                    }
                    podeSpawnar = true;
                }
            break;

            case 30:
                if(podeSpawnar == false){
                    for (int i = 3; i < 5; i++)
                    {
                        inimigos[i].SetActive(true);
                    }
                    podeSpawnar = true;
                }
            break;

            case 40:
                for (int i = 5; i < 7; i++)
                    {
                        inimigos[i].SetActive(true);
                    }
                    podeSpawnar = true;
            break;

            case 50:
                for (int i = 7; i < 12; i++)
                    {
                        inimigos[i].SetActive(true);
                    }
                    podeSpawnar = true;
            break;

            default:
                podeSpawnar = false;
            break;
        }
    }
}