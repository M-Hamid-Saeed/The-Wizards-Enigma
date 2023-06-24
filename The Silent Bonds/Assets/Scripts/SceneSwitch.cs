using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {
    [SerializeField] private string LevelName;
    [SerializeField] private Animator sceneSwitchAnimator;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger");

        StartCoroutine(LoadLevelAsync());
    }

    private IEnumerator LoadLevelAsync() {
        // Trigger the scene switch animation
        sceneSwitchAnimator.SetTrigger("switch");

        // Wait for a short delay to allow the animation to play
        yield return new WaitForSeconds(1f);
        
        // Start loading the next scene

        SceneManager.LoadSceneAsync(LevelName);
        /*Scene levelScene = SceneManager.GetSceneByName(LevelName);
        SceneManager.SetActiveScene(levelScene);
*/

    }
}
