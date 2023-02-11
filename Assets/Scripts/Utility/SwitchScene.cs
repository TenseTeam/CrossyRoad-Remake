using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    public float time = 10;
    public string sceneToLoad;

    public void ChangeScene()
    {
        StartCoroutine(LoadSceneIn());
    }

    private IEnumerator LoadSceneIn()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}