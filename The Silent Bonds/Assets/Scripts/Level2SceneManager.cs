using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2SceneManager : MonoBehaviour {
    private bool isTriggered = false; // Track if the trigger has been activated

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger");
      
         
            StartCoroutine(LoadLevel3Async());
        
    }

    private IEnumerator LoadLevel3Async() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level3");

        // Wait until Level 3 finishes loading
        while (!asyncLoad.isDone) {
            yield return null;
        }

        // Level 3 has finished loading, activate the scene
        Scene level3Scene = SceneManager.GetSceneByName("Level3");
        SceneManager.SetActiveScene(level3Scene);
        Time.timeScale = 1;
    }
}
