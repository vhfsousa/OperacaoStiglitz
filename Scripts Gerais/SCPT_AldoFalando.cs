using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCPT_AldoFalando : MonoBehaviour
{
    [SerializeField] private Image fundoAldo;
    [SerializeField] private Animator fundoAldoAnimator;
    [SerializeField] private Image aldo;
    [SerializeField] private Text aldoTexto;
    [SerializeField] private string textoParaAparecer;
    [SerializeField] private AudioSource audioSourceAldo;
    [SerializeField] private AudioClip[] vozesAldo;

    void Awake(){
        fundoAldoAnimator = fundoAldo.GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player")){
            StartCoroutine("AldoDialogo");
        }
    }

    private IEnumerator AldoDialogo(){
        //Tempo que ele aparece
        fundoAldo.gameObject.SetActive(true);
        aldoTexto.text = textoParaAparecer;
        fundoAldoAnimator.SetBool("Apareceu", true);
        yield return new WaitForSeconds(0.4f);
        audioSourceAldo.PlayOneShot(vozesAldo[Random.Range(0, vozesAldo.Length)]);
        aldo.gameObject.SetActive(true);

        //Tempo que ele fica na tela
        yield return new WaitForSeconds(10f);

        //Tempo que ele desaparece
        fundoAldoAnimator.SetBool("Apareceu", false);
        yield return new WaitForSeconds(0.5f);
        aldo.gameObject.SetActive(false);
        yield return null;
    }
}