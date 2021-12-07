using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCPT_ColliderCheckpoint : MonoBehaviour
{
    public string nomeDaChavePlayerPrefs;
    public int valorChavePlayerPrefs;

    void OnTriggerEnter (Collider col)
    {
        if(col.CompareTag("Player")){
            PlayerPrefs.SetInt(nomeDaChavePlayerPrefs, valorChavePlayerPrefs);
        }
    }
}