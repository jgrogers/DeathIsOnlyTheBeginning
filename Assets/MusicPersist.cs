using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPersist : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>(); 
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
