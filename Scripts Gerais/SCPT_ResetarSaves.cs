using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCPT_ResetarSaves : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            PlayerPrefs.SetFloat("Volume", 100);
            PlayerPrefs.SetFloat("Sensibilidade", 1);
            PlayerPrefs.SetInt("possuiP08", 1);
            PlayerPrefs.SetInt("possuiThompson", 0);
            PlayerPrefs.SetInt("possuiMG42", 0);
            PlayerPrefs.SetInt("checkpointFase1", 0);
            PlayerPrefs.SetInt("checkpointFase2", 0);
            PlayerPrefs.SetInt("checkpointFase3", 0);
            Debug.Log("As variáveis de save foram resetadas");
        }
    }
}