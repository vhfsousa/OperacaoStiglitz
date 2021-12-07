using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCPT_ColliderTrocarDeCenaCutscene1 : MonoBehaviour
{
    [SerializeField] private GameObject loadingGameObject;
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject cutscene1;
    [SerializeField] private AudioSource audioSourceCutscene;
    [SerializeField] private AudioClip somPorta;
    [SerializeField] private AudioClip somPassos;
    [SerializeField] private AudioClip somAldo;
    [SerializeField] private AudioClip somTiro;
    [SerializeField] private AudioClip somInimigoTomandoTiro;
    [SerializeField] private AudioClip somAldo2;
    [SerializeField] private AudioClip somAldo3;
    [SerializeField] private AudioClip somGranada;
    [SerializeField] private AudioClip somMedidor;
    [SerializeField] private AudioClip somAldo4;
    [SerializeField] private AudioClip somPorta2;
    [SerializeField] private AudioClip coletavelP08;
    [SerializeField] private Text textoCutscene1;
    [SerializeField] private string texto1;
    [SerializeField] private string texto2;
    [SerializeField] private string texto3;
    [SerializeField] private string texto4;

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            StartCoroutine("Cutscene1");
        }
    }

    IEnumerator Cutscene1(){
        cutscene1.SetActive(true);
        audioSourceCutscene.PlayOneShot(somPorta);
        yield return new WaitForSeconds(1.308f);
        audioSourceCutscene.PlayOneShot(somPassos);
        yield return new WaitForSeconds(2.100f);
        audioSourceCutscene.PlayOneShot(somAldo);
        textoCutscene1.text = texto1;
        yield return new WaitForSeconds(10.250f);
        textoCutscene1.text = "";
        audioSourceCutscene.PlayOneShot(somTiro);
        yield return new WaitForSeconds(4.430f);
        audioSourceCutscene.PlayOneShot(somTiro);
        yield return new WaitForSeconds(4.430f);
        audioSourceCutscene.PlayOneShot(somInimigoTomandoTiro);
        yield return new WaitForSeconds(0.750f);
        audioSourceCutscene.PlayOneShot(somAldo2);
        textoCutscene1.text = texto2;
        yield return new WaitForSeconds(5.500f);
        textoCutscene1.text = "";
        audioSourceCutscene.PlayOneShot(coletavelP08);
        yield return new WaitForSeconds(0.7f);
        audioSourceCutscene.PlayOneShot(somAldo3);
        textoCutscene1.text = texto3;
        yield return new WaitForSeconds(4.500f);
        textoCutscene1.text = "";
        audioSourceCutscene.PlayOneShot(somGranada);
        yield return new WaitForSeconds(4.756f);
        audioSourceCutscene.PlayOneShot(somMedidor);
        yield return new WaitForSeconds(0.486f);
        audioSourceCutscene.PlayOneShot(somAldo4);
        textoCutscene1.text = texto4;
        yield return new WaitForSeconds(10.250f);
        textoCutscene1.text = "";
        audioSourceCutscene.PlayOneShot(somPorta);
        yield return new WaitForSeconds(1.308f);
        StartCoroutine("ChangeScene", sceneName);
        yield return null;
    }

    IEnumerator ChangeScene(string sceneName){
        yield return new WaitForSecondsRealtime(1f);
        loadingGameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            yield return null;
        }
    }
}