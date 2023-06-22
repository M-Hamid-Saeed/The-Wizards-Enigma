using UnityEngine;

public class EagleSound : MonoBehaviour {
    public AudioClip soundClip;    
    public float minInterval = 5f;  
    public float maxInterval = 10f; 

    private AudioSource audioSource;
    private float nextPlayTime;    

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        SetNextPlayTime();
    }

    private void Update() {
        if (Time.time >= nextPlayTime) {
            PlaySound();
            SetNextPlayTime();
        }
    }

    private void PlaySound() {
        audioSource.PlayOneShot(soundClip);
    }

    private void SetNextPlayTime() {
        
        nextPlayTime = Time.time + Random.Range(minInterval, maxInterval);
    }
}
