using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderTrocaDeCena : MonoBehaviour
{
    [SerializeField] private GameObject loadingGameObject;
    [SerializeField] private string sceneName;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            StartCoroutine("ChangeScene", sceneName);
        }
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