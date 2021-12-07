using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCPT_CreditsVolume : MonoBehaviour
{
    void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");
    }
}