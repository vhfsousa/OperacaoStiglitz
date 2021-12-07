using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlavancaGrades : MonoBehaviour
{
    public GameObject[] grades;
    bool podeApertar;
    [SerializeField] private AudioSource somAlavancaSource;
    [SerializeField] private AudioClip clipSomAlavanca;
    [SerializeField] private AudioClip clipColetavel;
    [SerializeField] private GameObject bomba;

    void Start()
    {
        
    }

    void Update()
    {
        if (podeApertar)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                bomba.gameObject.SetActive(false);
                somAlavancaSource.PlayOneShot(clipSomAlavanca);
                for (int i = 0; i < grades.Length; i++)
                {
                    grades[i].GetComponent<BoxCollider>().enabled = false;
                    grades[i].gameObject.SetActive(false);
                }
                somAlavancaSource.PlayOneShot(clipColetavel);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            podeApertar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            podeApertar = false;
        }
    }
}