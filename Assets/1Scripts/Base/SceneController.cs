using System;
using System.Collections;
using UnityEditor;
using UnityEngine; 
using UnityEngine.SceneManagement;


public class SceneController
{
    public void LoadScene(int sceneNumber, Action callback)
    {
        CoroutineRunner.Instance. StartCoroutine(LoadSceneById(sceneNumber, callback));
    }

    public void LoadScene(string sceneName, Action callback)
    {
        CoroutineRunner.Instance.StartCoroutine(LoadSceneByName(sceneName, callback));
    }


    private IEnumerator LoadSceneById(int sceneNumber, Action callback)
    {
        Master.UIController.SetLoadingScreen();
        yield return new WaitForSecondsRealtime(0.975f);
        Debug.Log("# LoadSceneById " + sceneNumber);
        SceneManager.LoadSceneAsync(sceneNumber);
        EventsProvider.OnLoadingSceneValueChanged?.Invoke(1);


        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNumber);
        asyncOperation.allowSceneActivation = false;


        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                
                yield return null;

                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.1f);

        callback?.Invoke();
    }

    private IEnumerator LoadSceneByName(string sceneName, Action callback)
    {
        Master.UIController.SetLoadingScreen();
        yield return new WaitForSecondsRealtime(0.375f);
        Debug.Log("# LoadSceneById " + sceneName);
        SceneManager.LoadScene(sceneName);
        EventsProvider.OnLoadingSceneValueChanged?.Invoke(1);
        
        callback?.Invoke();
    }

    
}