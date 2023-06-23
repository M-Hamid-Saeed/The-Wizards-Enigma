using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalRedLevel1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger");


        StartCoroutine(LoadLevel2Async());

    }

    private IEnumerator LoadLevel2Async() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level2");

        // Wait until Level 3 finishes loading
        while (!asyncLoad.isDone) {
            yield return null;
        }

        // Level 3 has finished loading, activate the scene
        Scene level3Scene = SceneManager.GetSceneByName("Level2");
        SceneManager.SetActiveScene(level3Scene);
        
    }
}
