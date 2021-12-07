using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCPT_ColliderTrocarDeCenaCutscene2 : MonoBehaviour
{
    [SerializeField] private GameObject loadingGameObject;
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject cutscene2;
    [SerializeField] private AudioSource audioSourceCutscene;
    [SerializeField] private AudioClip somPassos;
    [SerializeField] private AudioClip somAldo;
    [SerializeField] private AudioClip somAldo2;
    [SerializeField] private Text textoCutscene2;
    [SerializeField] private string texto1;
    [SerializeField] private string texto2;

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            StartCoroutine("Cutscene2");
        }
    }

    IEnumerator Cutscene2(){
        cutscene2.SetActive(true);
        audioSourceCutscene.PlayOneShot(somPassos);
        yield return new WaitForSeconds(2.100f);
        audioSourceCutscene.PlayOneShot(somAldo);
        textoCutscene2.text = texto1;
        yield return new WaitForSeconds(6f);
        audioSourceCutscene.PlayOneShot(somAldo2);
        textoCutscene2.text = texto2;
        yield return new WaitForSeconds(11f);
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