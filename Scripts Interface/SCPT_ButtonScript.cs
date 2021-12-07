using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SCPT_ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject loadingGameObject;
    [SerializeField] private GameObject[] objetosAtivaveis;
    [SerializeField] private GameObject[] objetosDesativaveis;


    public void ClickToChangeScene(string sceneName){
        StartCoroutine("ChangeScene", sceneName);
    }

    public void ClickToDisableAndEnableGameObjects(){
        StartCoroutine("DisableAndEnableGameObjects");
    }

    public void ClickToExit()
    {
        StartCoroutine("ExitScene");
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

    IEnumerator DisableAndEnableGameObjects(){
        yield return new WaitForSecondsRealtime(1f);
        foreach (GameObject objetoAtivavel in objetosAtivaveis)
        {
            objetoAtivavel.gameObject.SetActive(true);
        }

        foreach (GameObject objetoDesativavel in objetosDesativaveis)
        {
            objetoDesativavel.gameObject.SetActive(false);
        }
        yield return null;
    }

    IEnumerator ExitScene(){
        yield return new WaitForSecondsRealtime(1f);
        Application.Quit();
        yield return null;
    }
}