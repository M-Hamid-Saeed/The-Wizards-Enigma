using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrompt : MonoBehaviour
{

    [SerializeField] private GameObject promptContainer;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShowPrompt", 10, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowPrompt()
    {
        promptContainer.SetActive(true);
        StartCoroutine(HidePrompt(3));
    }

    IEnumerator HidePrompt(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        promptContainer.SetActive(false);
    }
}
