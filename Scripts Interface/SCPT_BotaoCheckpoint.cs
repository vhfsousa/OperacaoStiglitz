using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCPT_BotaoCheckpoint : MonoBehaviour
{
    public string nomeDaChavePlayerPrefs;
    public int valorChavePlayerPrefs;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string sceneName;

    void Start()
    {
        if(PlayerPrefs.GetInt(nomeDaChavePlayerPrefs) != 0 && PlayerPrefs.GetInt(nomeDaChavePlayerPrefs) <= valorChavePlayerPrefs)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void OnButtonClick()
    {
        StartCoroutine("ChangeScene", sceneName);
        PlayerPrefs.SetInt(nomeDaChavePlayerPrefs, valorChavePlayerPrefs);
    }

    IEnumerator ChangeScene(string sceneName){
        yield return new WaitForSecondsRealtime(1f);
        loadingScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            yield return null;
        }
    }
}